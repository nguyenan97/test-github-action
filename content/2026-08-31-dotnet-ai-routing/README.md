# .NET AI Routing and Failover

This content asset explores one architectural point:

> Application code should depend on `IChatClient`; model/provider selection and failover should live behind routing infrastructure.

## Why this matters

`Microsoft.Extensions.AI` 10.9.0 introduced experimental routing primitives including `RoutingChatClient`, `SemanticRoutingChatClient`, `FailoverChatClient`, and `OrderedFailoverChatClient`.

The useful architectural boundary is not simply model selection. It is keeping routing policy outside application code so cost, capability, health, latency, tenant policy, and provider failover can evolve independently.

## Architecture

```text
Application
    |
    v
IChatClient
    |
    +-- Routing policy
    |      +-- cost
    |      +-- capability
    |      +-- health / latency
    |      +-- request semantics
    |
    +-- Provider A
    +-- Provider B
```

## Production caveats

- These routing APIs are currently experimental (`MEAI001`).
- Failover can retry when a provider fails before output is exposed. Once streaming output has been committed to the caller, transparent failover cannot continue the response from another provider.
- Stateful conversations need a routing-affinity strategy. Switching providers/models between turns can lose provider-specific reasoning state or prompt-cache reuse.

## Demo scope

A useful implementation demo should prove three scenarios:

1. Route ordinary and complex requests to different `IChatClient` implementations.
2. Fail over to a backup client when the primary fails before returning output.
3. Demonstrate/document the streaming failure boundary.

Production extensions can add sticky routing, provider health scoring, circuit breaking, tenant budgets, region/data-residency constraints, capability filtering, and OpenTelemetry.

## Sources

- Microsoft .NET Blog — Routing and Failover for Microsoft.Extensions.AI, published 2026-08-13.
- Microsoft.Extensions.AI 10.9.0 API documentation.

Re-check official documentation before using the experimental APIs in production.