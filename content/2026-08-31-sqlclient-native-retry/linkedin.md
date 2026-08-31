Microsoft.Data.SqlClient can retry transient SQL failures natively. The harder question is whether it should own your retry policy.

Microsoft's Azure SQL team highlighted Configurable Retry Logic again this week. It can attach retry providers directly to `SqlConnection` and `SqlCommand`, with fixed, incremental, or exponential delays and built-in jitter.

That makes one architecture decision worth revisiting in .NET services that already use Polly or another resilience layer.

I would treat SqlClient retry as a narrow database-boundary mechanism, not as a replacement for application resilience.

Connection retry is relatively easy to reason about. A transient failure while opening a connection is exactly the kind of SQL-specific condition the driver understands well.

Command retry needs more care.

The connection and command retry providers are independent, and Microsoft's guidance explicitly calls out idempotency. Built-in command retry also does not retry commands running inside an open transaction. For transactional work, the safer recovery boundary is usually the entire transaction, not one statement in the middle of it.

There is another failure mode that is easy to create accidentally: retry amplification.

Imagine this stack:

`API retry -> Polly retry -> SqlClient retry -> Azure SQL`

If every layer gets its own retry count and backoff policy, an outage can produce much more database traffic than the configuration suggests locally. A transient incident becomes a retry storm.

So the design I prefer is to make the retry budget explicit:

- SqlClient handles SQL-specific connection failures and carefully selected safe commands.
- Application resilience handles transactions and cross-service workflows.
- Non-idempotent operations require an explicit replay strategy.
- Retry events are observable; a request that succeeded after three retries is still operationally interesting.

The API is straightforward. Deciding where retry responsibility belongs is the real engineering problem.

#dotnet #azure #sqlserver #resilience #softwarearchitecture