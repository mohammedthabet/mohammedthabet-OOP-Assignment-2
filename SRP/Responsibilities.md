# SRP Responsibilities Analysis

## 1. WardBoard

### Responsibilities found
- Maintain bed and patient state.
- Calculate patient acuity.
- Generate escalation pager messages.
- Format handoff notes.
- Export census data as CSV.

### Why this violates SRP
These responsibilities change for different reasons: clinical scoring rules,
paging rules, handoff presentation, and export format can evolve independently.

### Refactoring
- `WardBoard` — owns ward/bed state and coordinates ward operations.
- `AcuityScorer` — calculates patient acuity.
- `WardPager` — handles escalation pager messages.
- `HandoffFormatter` — formats handoff notes.
- `CensusExporter` — exports census data.

---

## 2. CheckoutBasket

### Responsibilities found
- Maintain basket lines.
- Interpret coupon text and calculate pricing.
- Handle gift-wrap pricing.
- Format gift messages.
- Simulate payment authorization.

### Why this violates SRP
Basket state, marketing coupon rules, packaging pricing, customer-facing
messages, and payment processing can change independently.

### Refactoring
- `CheckoutBasket` — owns basket state and pricing-related operations.
- `GiftCardFormatter` — formats gift messages.
- `PaymentAuthorizer` — performs payment authorization behavior.

---

## 3. SupportTicket

### Responsibilities found
- Maintain ticket data and conversation state.
- Classify ticket priority.
- Calculate SLA deadlines.
- Format public and internal support messages.

### Why this violates SRP
Ticket state, priority rules, SLA policy, and communication wording are
independent reasons to change.

### Refactoring
- `CheckoutBasket` — owns basket state and coordinates basket totals.
- `CouponPolicy` — interprets coupon text and calculates discounts.
- `GiftWrapPricingPolicy` — applies gift-wrap pricing policy.
- `GiftCardFormatter` — formats customer-facing gift messages.
- `PaymentAuthorizer` — performs payment authorization behavior.

---

## 4. LoanDesk

### Responsibilities found
- Hold loan application data.
- Calculate risk and eligibility.
- Determine required compliance documents.
- Write customer decision letters.
- Export underwriting data.

### Why this violates SRP
Risk policy, compliance requirements, customer communication, and analytics
formats can evolve independently.

### Refactoring
- `LoanDesk` — represents loan application data.
- `LoanRiskEvaluator` — evaluates risk and eligibility.
- `LoanDocumentPolicy` — determines required documents.
- `LoanDecisionLetterWriter` — formats decision letters.
- `LoanCsvExporter` — exports underwriting data.

---

## 5. CourseEnrollmentDesk

### Responsibilities found
- Manage seats and waitlists.
- Generate welcome material.
- Format tuition invoice information.

### Why this violates SRP
Enrollment policy, course communication, and finance formatting have
different reasons to change.

### Refactoring
- `CourseEnrollmentDesk` — manages enrollment and waitlist state.
- `WelcomePacketWriter` — generates student welcome material.
- `TuitionInvoiceFormatter` — formats tuition invoice information.

---

## 6. KitchenTicket

### Responsibilities found
- Maintain kitchen order items.
- Detect allergens.
- Calculate preparation ETA.
- Render thermal-printer tickets.
- Select an expo lane.

### Why this violates SRP
Menu/allergen rules, ETA rules, printer layout, and routing rules can change
independently.

### Refactoring
- `KitchenTicket` — owns order state and coordinates ticket operations.
- `AllergenDetector` — detects allergens.
- `KitchenEtaCalculator` — calculates preparation ETA.
- `ThermalTicketRenderer` — renders printer output.
- `ExpoLaneRouter` — selects the expo lane.

---

## 7. SubscriptionBilling

### Responsibilities found
- Maintain subscription billing state.
- Calculate prorated charges.
- Generate invoice numbers.
- Write dunning emails.
- Export ledger information.

### Why this violates SRP
Billing calculations, numbering rules, collections communication, and
accounting integration are separate reasons to change.

### Refactoring
- `SubscriptionBilling` — owns subscription state and proration behavior.
- `InvoiceNumberGenerator` — generates invoice numbers.
- `DunningEmailWriter` — creates collections emails.
- `LedgerExporter` — exports accounting data.

---

## 8. WarehousePickList

### Responsibilities found
- Maintain warehouse pick requirements.
- Allocate available stock.
- Determine walking order.
- Generate picker instructions.
- Export data to the WMS.

### Why this violates SRP
Inventory allocation, warehouse routing, picker UX, and WMS integration
can evolve independently.

### Refactoring
- `WarehousePickList` — owns pick-list state and coordinates operations.
- `PickNeed` — represents one picking requirement.
- `StockAllocator` — calculates allocated quantities and shortages.
- `WalkingRoutePlanner` — determines picking order.
- `PickerScriptWriter` — creates human-readable picking instructions.
- `WmsExporter` — exports WMS data.

---

## 9. GradeBook

### Responsibilities found
- Store student scores and calculate averages.
- Apply grading and honor-roll policy.
- Format transcripts.
- Export grade data.

### Why this violates SRP
Score storage, academic policy, transcript presentation, and export formats
can change independently.

### Refactoring
- `GradeBook` — stores scores and calculates averages.
- `GradePolicy` — applies letter-grade and honor-roll rules.
- `TranscriptFormatter` — formats transcripts.
- `GradeCsvExporter` — exports grade information.

---

## 10. AppointmentDesk

### Responsibilities found
- Manage appointment slots and bookings.
- Apply business-hours policy.
- Serialize appointments to ICS.
- Format reminder messages.

### Why this violates SRP
Scheduling, clinic-hours policy, calendar interoperability, and reminder
wording have separate reasons to change.

### Refactoring
- `AppointmentDesk` — manages appointment bookings.
- `BusinessHoursPolicy` — applies opening-hours rules.
- `IcsCalendarExporter` — generates ICS calendar data.
- `AppointmentReminderFormatter` — formats reminder messages.