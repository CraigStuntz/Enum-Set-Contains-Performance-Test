Enum Set Performance Comparison
===============================

I need to test if a some enum values are members of a set of enums. A lot.

There's a substantial difference in performance based on the data structure you use.

Here's one run on my development machine:

<table>
<tr><td>HashSet:           </td><td>35.9 ms </td></tr>
<tr><td>F# set:            </td><td>1528 ms</td></tr>
<tr><td>C#:                </td><td>116.2 ms</td></tr>
<tr><td>C# (int input):    </td><td>14.58 ms</td></tr>
<tr><td>F# (generic enum): </td><td>42.7 ms</td></tr>
<tr><td>F# (IConvertible): </td><td>123.7 ms</td></tr>
<tr><td>F# (int input):    </td><td>12.83 ms</td></tr>
</table>