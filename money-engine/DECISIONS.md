# Decisions

## 2026-08-31

### D-001 - Bootstrap Money Engine inside sandbox repository

Status: active

Decision:
Use `nguyenan97/test-github-action` as a temporary host because the current GitHub connector does not expose repository creation. Keep the project isolated under `money-engine/` so it can be migrated to a dedicated repository later.

Reason:
This allows the autonomous workflow, agent instructions and persistent state model to be built now without touching production/private repositories.

Revisit when:
A repository-creation capability becomes available or the user creates a dedicated repository.

### D-002 - Codex integration model

Status: active

Decision:
Use repository-level `AGENTS.md` as the execution contract for Codex/code agents. Do not claim a live Codex session is attached when no explicit Codex execution tool is available in the current environment.

Reason:
The GitHub connector supports the repository operations needed by Codex workflows, but this session does not expose a separate Codex run/start action.

Revisit when:
A Codex execution action becomes available in the product/tooling.
