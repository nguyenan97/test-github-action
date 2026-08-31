# LinkedIn draft

Your .NET AI application probably should not depend on a single model endpoint.

`Microsoft.Extensions.AI` 10.9.0 now includes experimental routing primitives such as `RoutingChatClient`, `SemanticRoutingChatClient`, `FailoverChatClient`, and `OrderedFailoverChatClient`.

The interesting part is not the extra abstraction. It is where the routing policy can now live.

Instead of spreading provider decisions across application code, the application can keep talking to `IChatClient`, while routing becomes infrastructure.

That creates a clean boundary for policies such as cost, model capability, provider health, latency, tenant budgets, region constraints, and request semantics.

There are two catches I would keep in mind before treating this as a production LLM gateway.

First, the APIs are still experimental (`MEAI001`).

Second, failover has a hard boundary with streaming. A `FailoverChatClient` can retry when a provider fails before output is committed. Once streaming output has been exposed to the caller, that failure is terminal. You cannot transparently jump to another provider halfway through the answer.

Semantic routing also raises a session-design question. Re-routing every turn can be a bad idea for stateful conversations because provider-specific reasoning state and prompt caching do not necessarily survive a model switch.

I would separate two decisions:

- select the route for a conversation or workload;
- handle provider failure inside that route.

That leaves a cleaner place to add sticky routing, health checks, budgets, region constraints, and observability later.

The abstraction is small. The architecture decision behind it is not.

#dotnet #azure #ai