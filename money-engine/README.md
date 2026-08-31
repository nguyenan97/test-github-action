# Money Engine

An autonomous research-to-product system for discovering technically defensible opportunities in .NET, Azure, AI and developer tooling, then turning the strongest ones into validated assets, experiments and products.

## Core loop

1. Discover fresh opportunities from primary sources and developer pain signals.
2. Verify demand, alternatives, willingness-to-pay evidence and technical feasibility.
3. Score each opportunity and kill weak ideas early.
4. Convert strong ideas into either content, a reusable developer asset, or a product experiment.
5. Use code agents only after the opportunity passes validation gates.
6. Open Draft PRs instead of changing main directly.
7. Measure evidence from users, GitHub activity, leads or product usage.
8. Re-score, improve, pause or kill the experiment.

## Decision gates

- BUILD: score >= 80 with credible demand evidence and a narrow MVP.
- WATCH: score 65-79 or evidence is incomplete.
- SKIP: score < 65, weak differentiation, no buyer, or poor economics.

## Repository state

- `AGENTS.md`: instructions for Codex/code agents.
- `docs/OPERATING_MODEL.md`: autonomous decision rules.
- `opportunities/`: research dossiers and scoring.
- `products/`: validated product experiments.
- `content/`: distribution assets linked to a technical thesis.
- `metrics/`: experiment outcomes and weekly decisions.
- `DECISIONS.md`: persistent decision log.

## Safety and quality boundaries

- Always research fresh information before making current claims.
- Prefer primary sources for technical facts.
- Do not invent demand, benchmarks, customer stories, APIs or versions.
- Do not build simply because an idea is technically interesting.
- No direct writes to `main`.
- No auto-merge.
- No fake personal experience in public content.
- Avoid em dashes in generated prose.
