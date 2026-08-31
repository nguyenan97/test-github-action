# AGENTS.md

## Mission

Turn verified engineering pain points into small, testable assets or products with a credible path to revenue.

## Operating rules

1. Research first. For anything current, verify with fresh web research before coding.
2. Prefer official technical sources for API, version, maturity and behavior claims.
3. Do not build ideas with weak buyer evidence. Record them under `opportunities/` as WATCH or SKIP.
4. For BUILD candidates, create a narrow specification before implementation.
5. Keep implementation small enough to validate one commercial hypothesis.
6. Add tests for important behavior. If an external API cannot be verified, do not invent it.
7. Never write directly to `main`. Work on a branch and open a Draft PR.
8. Never auto-merge.
9. Record major choices in `DECISIONS.md` so future agents do not restart from zero.
10. Public writing must not fabricate personal experience, production incidents, benchmarks, users or revenue.
11. Avoid em dashes in generated prose. Prefer plain punctuation and natural sentence structure.

## Required workflow for a new opportunity

Create `opportunities/YYYY-MM-DD-<slug>.md` with:

- Problem
- Target buyer
- Evidence
- Existing alternatives
- Why current alternatives are insufficient
- Willingness-to-pay signal
- Technical moat
- MVP
- Distribution path
- Operating cost
- Risks
- Score /100
- Decision: BUILD / WATCH / SKIP

Only BUILD candidates may create a project under `products/`.

## Required workflow for a BUILD candidate

Create:

- `products/<slug>/SPEC.md`
- `products/<slug>/ARCHITECTURE.md`
- `products/<slug>/ROADMAP.md`
- minimal source code and tests when justified

The specification must define one measurable hypothesis. Example:

> Teams with nested retry policies need a static analyzer that identifies retry amplification and non-idempotent replay risk before deployment.

Do not expand scope until the first hypothesis has evidence.

## Review checklist

Before opening a Draft PR, verify:

- Current facts are sourced.
- Maturity status is explicit.
- No unsupported benchmarks or claims.
- At least one limitation is documented.
- Code compiles or clearly states what could not be verified.
- The README explains what evidence would cause the experiment to be killed.
