# Development notes

Running log of problems and decisions. Two lines each: what happened, what I did.

This is where the "Challenges encountered" section of Milestone 2 and the "Reflection on
development" section of Milestone 3 come from. Write it down the day it happens — it
cannot be reconstructed six weeks later.

---

### 23 Aug 2026 — project setup

No .NET SDK on the machine, only the .NET 6 runtime, so nothing would build. Installed the
.NET 8 SDK and the Windows Desktop runtime to `C:\Users\aashi\.dotnet` using the per-user
install script, because I do not have admin rights on this machine.

### 23 Aug 2026 — database design changed from the proposal

The proposal had three tables including a separate `Categories` table. I dropped it and made
category a plain text column on `StockItems`. A lookup table meant a join and an extra form
to manage categories, for no real benefit at this size. Updated the proposal to match.

---

<!--
Add new entries at the bottom, newest last. Format:

### <date> — <short title>
What went wrong or what I decided, in two or three sentences. What I did about it.
-->
