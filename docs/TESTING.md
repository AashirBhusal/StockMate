# Testing

Test run of 4 October 2026, on Windows 11 with .NET 8, against the Debug build.

## How it was tested

- The tests used a **separate copy of the program with its own empty database**, so the demo
  data was never touched.
- Each scenario was carried out through the real windows (the add form, the movement form and
  so on). The clicks and typing were done by a script using Windows UI Automation, so the same
  steps can be repeated exactly. The script was written with help from Claude (see the README).
- After each group, the database was read with a separate SQLite query to check that what the
  screen showed was what had actually been saved.
- The script could not type into the date pickers. The date cases in section 2 were set up by
  putting rows straight into the test database; the date pickers themselves were only used with
  their default value (today).

**Result: 38 scenarios. 36 passed first time. 2 failed, were fixed, and passed when re-run.**

## 1. Adding and editing items (FR-01, FR-02)

| # | Scenario | Expected | What happened | Result |
|---|---|---|---|---|
| 1 | First run, no database file | File is created, list is empty | `stockmate.db` created; empty list; "Nothing needs attention today." | Pass |
| 2 | Save with a blank name | Message, form stays open | "Item name cannot be blank." Form stayed open, nothing saved | Pass |
| 3 | Name of only spaces | Treated as blank | "Item name cannot be blank." | Pass |
| 4 | Blank category | Message | "Category cannot be blank." | Pass |
| 5 | Add a consumable: "Hand soap, pump 500ml", 20 bottles, reorder at 5, $3.50 | Appears in the list and is saved | Listed with the right values; row found in the database | Pass |
| 6 | Choose Durable in the type list, add "Bath towels", 62, reorder at 20, 24 months | Use-by field swaps for the replacement fields; item saved | Use-by field hidden, replacement fields shown; item listed and saved | Pass |
| 7 | Edit a durable that is not the first row; change reorder level 5 to 8 | Stored values shown; type and quantity locked; change saved | Name and 24 months shown; type and quantity disabled; database shows 8 | Pass |
| 8 | Edit a consumable | Use-by field shown, replacement fields hidden | As expected | Pass |
| 9 | Blank the name while editing, then Cancel | Message; item unchanged | "Item name cannot be blank."; name unchanged in the database | Pass |
| 10 | Deactivate, answer No, then again and answer Yes | No: nothing changes. Yes: item leaves the list but is not deleted | Still listed after No. Gone after Yes; row still in the database with `IsActive = 0` | Pass |

When the deactivate button was built (23 September) the same check was made on an item with
three movements: all three `Transactions` rows were still there afterwards.

## 2. Start-up warnings (FR-06)

| # | Item set up as | Expected | Warning shown | Result |
|---|---|---|---|---|
| 11 | Use-by in 10 days | Warn | "…expires in 10 day(s) - use it first." | Pass |
| 12 | Use-by in 30 days (the limit) | Warn | "…expires in 30 day(s) - use it first." | Pass |
| 13 | Use-by in 31 days | No warning | None | Pass |
| 14 | Use-by in 6 months | No warning | None | Pass |
| 15 | Use-by yesterday | Warn as expired | "…has expired - remove it from the storeroom." | Pass |
| 16 | Use-by today (item added through the form with the default date) | Warn | First showed "expires in 0 day(s)", which reads badly. Changed to "…expires today - use it first." | Pass |
| 17 | Durable replaced 3 years ago, 24-month interval | Warn | "…was due for replacement on 04/10/2025." | Pass |
| 18 | Durable replaced exactly 24 months ago (the limit) | Warn | "…was due for replacement on 04/10/2026." | Pass |
| 19 | Durable replaced 1 year ago, 24-month interval | No warning | None | Pass |
| 20 | Quantity equal to the reorder level (10 and 10) | Warn | "…is low: 10 each left." | Pass |
| 21 | Quantity one above the reorder level (11 and 10) | No warning | None | Pass |

## 3. Receipts and issues (FR-03, FR-04)

| # | Scenario | Expected | What happened | Result |
|---|---|---|---|---|
| 22 | Receipt of 20 onto 62 | 82 | 82; movement saved with the staff name | Pass |
| 23 | Issue 100 with 82 on hand | Refused with a reason; nothing changes | "Cannot issue 100 of "Bath towels" because only 82 are on hand." Still 82; no movement saved | Pass |
| 24 | Save a movement with no staff name | Message; form stays open | "Please enter your name." Form stayed open; nothing saved | Pass |
| 25 | Issue exactly 82 of 82 | Allowed, ends at zero | 0; warning "Bath towels is low: 0 each left." | Pass |
| 26 | Issue 1 from 0 | Refused | "Cannot issue 1 of "Bath towels" because only 0 are on hand." | Pass |
| 27 | Receipt of 6 on a row that is not the first, staff name typed with spaces around it | The right item changes; name saved without the spaces | Form showed the right item; 10 became 16; its low-stock warning cleared; name saved as `Rita` | Pass |

Database check afterwards: one `Transactions` row for each saved movement, and none for the
refused or cancelled ones.

## 4. History, filter and export (FR-05, FR-07, FR-08)

| # | Scenario | Expected | What happened | Result |
|---|---|---|---|---|
| 28 | History of an item with two movements | Newest first, totals correct | Issue 82 above Receipt 20; "0 each on hand. 2 movement(s): received 20, issued 82 (net -62)." | Pass |
| 29 | Copy as text | Item line then one line per movement | 3 lines; first was "Bath towels (Durable) - 0 each on hand, replace by 04/10/2028" | Pass |
| 30 | History of an item with no movements | A plain message, no error | "No receipts or issues have been recorded for this item yet." | Pass |
| 31 | Filter by one category | Only that category | The 5 Breakfast items and nothing else | Pass |
| 32 | Export with the filter on | Only the filtered items | 5 items; total $447.50, which is the sum of the five lines | Pass |
| 33 | Export everything | One line per item, total correct | 12 items; total $3,985.50, equal to `SUM(QuantityOnHand * UnitCost)` worked out separately in SQL. The name containing a comma was written inside quotes | Pass |
| 34 | Export to a file another program has open | A message, not a crash | "The file could not be written. If it is open in Excel, close it and try again." Program kept running | Pass |

The export was checked by reading the file as text. It was not opened in Excel during this run.

## 5. Keeping the data safe

| # | Scenario | Expected | What happened | Result |
|---|---|---|---|---|
| 35 | Close and reopen | Data still there | Same items and quantities | Pass |
| 36 | Start the same program from a different folder | Same data | **Empty list, and a second `stockmate.db` appeared in that folder.** Fixed, then re-run: all 12 items shown, no stray file | Fail, then Pass |
| 37 | Database file is damaged (replaced with a text file) | A simple message | "StockMate could not start: SQLite Error 26: 'file is not a database'." Program then closed normally | Pass |

## 6. Very large quantity

| # | Scenario | Expected | What happened | Result |
|---|---|---|---|---|
| 38 | Receipt of 100,000 onto 50, then press Edit | Edit form opens | **"Unhandled exception has occurred in your application… Value of '100050' is not valid for 'Value'."** The edit form never opened and the program closed. Fixed, then re-run: the form opened showing 100050 and saved normally | Fail, then Pass |

## 7. Speed with 2,000 items

My proposal said the list should appear within one second of opening, and that saving a receipt
or an issue should take less than one second, for up to about 2,000 items. A separate test
database was filled with 2,000 items (775 of them needing attention).

| Measurement | Time | Against the target |
|---|---|---|
| Start the program until its window is on screen | 729 to 795 ms over three runs | Met |
| Refresh (re-reads the database and refills the list and the warnings; the same work is done after every saved movement) | 1,414 to 1,628 ms over three presses | **Not met** |
| Filter to one category (200 rows) | 117 ms | Met |
| Filter back to all 2,000 | 520 ms | Met |

These figures are only a guide. A repeat later, while the machine was busy, was slower (start-up
1.0 to 1.4 s, Refresh 1.8 to 2.8 s). One quick change was tried, pausing the warning list's
repainting while it fills, but the timings were too uneven to show that it helped, so it was
taken out again.

## Problems found and fixed

| Found | Problem | Fix |
|---|---|---|
| 21 Sep | On the movement form the question label covered the quantity box and hid the first digits | Box and buttons moved, form widened |
| 4 Oct (36) | The database was looked for in whatever folder the program was started from, not next to the program as the proposal said | `Program.cs` now builds the path from `AppContext.BaseDirectory` |
| 4 Oct (38) | Editing an item holding more than 100,000 crashed, because the read-only quantity box still had a maximum of 100,000 | `ItemForm` raises the maximum before showing the value |
| 4 Oct (16) | "expires in 0 day(s)" | Now says "expires today" |

## Known limits (seen, not fixed)

- With 2,000 items, Refresh and saving a movement take about 1.5 seconds or more, over my
  one-second target, because the whole list is reloaded after every change.
- A new consumable starts with today as its use-by date, so it shows "expires today" until the
  real date is entered.
- One movement can be at most 100,000 units.
- The date pickers were not driven by the test script (see "How it was tested").
- Tested on one Windows 11 computer only.
