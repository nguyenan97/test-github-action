# Native SQL retry in Microsoft.Data.SqlClient: useful, but not a Polly replacement

## Thesis

For .NET services talking to SQL Server or Azure SQL, `Microsoft.Data.SqlClient` can own narrowly scoped transient retry at the connection/command boundary, but retry safety still belongs to application architecture: transactions, idempotency, retry budgets, observability, and end-to-end resilience cannot be delegated to the driver.

## Why this is timely

Microsoft's Azure SQL team highlighted configurable retry logic again on 2026-08-28. The capability itself is not brand new: it has existed since Microsoft.Data.SqlClient 3.0 and became production-supported in 4.0. The useful engineering question is therefore not "what new retry API appeared?" but "where should retry responsibility live in a modern .NET service?"

## Verified facts

1. Configurable Retry Logic (CRL) supports selected transient failures for both `SqlConnection` and `SqlCommand`.
2. CRL is disabled by default. A retry provider must be assigned to a connection, a command, or configured as an application default.
3. Connection and command providers are independent. Assigning a provider to a `SqlConnection` does not automatically apply it to commands created from that connection.
4. Built-in providers include fixed, incremental and exponential retry policies and add jitter to reduce synchronized retries.
5. Built-in command retry does not retry a command executing inside an open transaction. Microsoft also recommends retrying an entire transaction rather than one statement inside it.
6. Command retry should be restricted to idempotent operations, or operations protected by an application-level idempotency mechanism.
7. `NumberOfTries` is the total number of attempts, including the initial operation.
8. The `Retrying` event should be observed/logged so recovered transient faults remain visible operationally.

## Architecture angle

A reasonable boundary is:

```text
HTTP / Worker
    |
Application use case
    |
Transaction + idempotency policy
    |
Repository / data access
    |
SqlCommand retry (only safe operations)
    |
SqlConnection retry
    |
Azure SQL / SQL Server
```

Driver-level retry is attractive because the driver knows SQL-specific transient error semantics. That does not mean every resilience concern should move into SqlClient.

Keep broader policies outside the driver when they cover more than SQL: HTTP calls, queues, business transactions, circuit breaking, request-level time budgets, fallback, or coordinated retries across dependencies.

## Example

```csharp
using Microsoft.Data.SqlClient;

var options = new SqlRetryLogicOption
{
    NumberOfTries = 4,
    DeltaTime = TimeSpan.FromSeconds(1),
    MaxTimeInterval = TimeSpan.FromSeconds(10)
};

var retryProvider =
    SqlConfigurableRetryFactory.CreateExponentialRetryProvider(options);

await using var connection = new SqlConnection(connectionString)
{
    RetryLogicProvider = retryProvider
};

await connection.OpenAsync();
```

This example deliberately configures connection-open retry only. Command retry needs a separate provider and a stronger safety decision because replaying a command may duplicate side effects.

## Failure modes to think about

### Retry amplification

If SqlClient retries three times, an outer Polly/resilience pipeline retries three times, and an upstream caller also retries, a single request can multiply database pressure during an outage. Define one retry budget rather than stacking defaults blindly.

### Transactions

Retrying one failed statement in the middle of a transaction is generally the wrong recovery boundary. Reconstruct and retry the transaction as a unit when the business operation is safe to replay.

### Non-idempotent commands

An INSERT, payment mutation, sequence allocation, message acknowledgement, or stored procedure with side effects cannot be assumed safe merely because the failure looks transient.

### Observability

A request that eventually succeeds after several SQL retries is not equivalent to a healthy request. Record retry count, error number and accumulated delay.

## Practical conclusion

Use SqlClient CRL where the retry boundary genuinely belongs to SQL connectivity or a demonstrably safe command. Keep application-level resilience for workflows, transactions and cross-service behavior. The main design task is preventing duplicate retry layers from turning a transient Azure SQL incident into a retry storm.

## Product hypothesis

There is a potentially reusable developer-tool angle here: a .NET resilience analyzer or architecture check that detects nested retry policies (SqlClient + EF/provider + Polly + HTTP/job retry), calculates worst-case retry amplification, and flags non-idempotent retry boundaries. This needs demand validation before building.

## Official sources

- Microsoft Azure SQL Dev Corner, 2026-08-28: `Try the new SqlClient and Retry connections natively`
- Microsoft Learn: `Configurable retry logic in SqlClient`
- Microsoft Learn: `Internal retry logic providers in SqlClient`
- Microsoft.Data.SqlClient 4.0 release notes: configurable retry logic became production-supported while remaining opt-in
