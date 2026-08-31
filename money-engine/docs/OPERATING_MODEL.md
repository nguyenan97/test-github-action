# Operating Model

## Goal

Maximize validated commercial learning per unit of engineering effort.

## Daily cycle

### 1. Discover

Search fresh technical changes and repeated pain signals across .NET, Azure, AI, architecture, developer tooling and B2B engineering workflows.

### 2. Challenge

For every promising idea, actively search for reasons it should not be built:

- incumbent products already solve it well
- buyer has no budget
- problem is infrequent
- switching cost is too high
- cloud/platform vendor already provides it
- required integrations are fragile
- support burden is too high
- distribution path is unclear

### 3. Score

Use a 100 point score:

- demand evidence: 20
- evidence quality: 15
- willingness to pay: 15
- technical differentiation: 15
- MVP feasibility: 10
- distribution access: 10
- recurring revenue potential: 10
- operating economics: 5

Decision thresholds:

- BUILD >= 80
- WATCH 65-79
- SKIP < 65

A score cannot compensate for zero buyer evidence.

### 4. Choose the smallest next action

Possible next actions:

- research one missing assumption
- publish technical content to test interest
- create a free diagnostic tool
- create a landing-page specification
- implement a narrow MVP
- stop the idea

Avoid full-product builds as the first experiment.

### 5. Execute through GitHub

Every material change should have a branch and Draft PR. Public-facing content and code must be auditable from sources.

### 6. Record outcomes

After an experiment, update metrics and `DECISIONS.md` with:

- what was expected
- what happened
- evidence
- next decision
- what would reverse the decision

## Product promotion rule

A technical topic can move through these states:

`CONTENT -> OPPORTUNITY -> EXPERIMENT -> PRODUCT`

Promotion requires new evidence at each boundary.

## Kill rule

Kill or pause an idea when one of these holds:

- repeated research cannot find credible users with the problem
- strong incumbents solve it at acceptable cost
- the buyer and user are disconnected with no sales path
- required support makes the economics unattractive
- the experiment receives no meaningful signal after a defined test window

Do not preserve an idea because code has already been written.
