Enum Set Performance Comparison
===============================

I need to test if a some enum values are members of a set of enums. A lot.

There's a substantial difference in performance based on the data structure you use.

Here's one run on my development machine:

<table>
<tr><td>F# set:            </td><td>543 ms</td></tr>
<tr><td>HashSet:           </td><td>20 ms </td></tr>
<tr><td>C#:                </td><td>120 ms</td></tr>
<tr><td>F# (generic enum): </td><td>37 ms </td></tr>
<tr><td>F# (IConvertible): </td><td>125 ms</td></tr>
<tr><td>F# (int input):    </td><td>13 ms </td></tr>
</table>