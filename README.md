# StairsAutomation

![Revit API](https://img.shields.io/badge/Revit%20API-2019.1-blue.svg)
![Platform](https://img.shields.io/badge/platform-Windows-lightgray.svg)
![.NET](https://img.shields.io/badge/.NET-4.7-blue.svg)
[![License](http://img.shields.io/:license-mit-blue.svg)](http://opensource.org/licenses/MIT)

C# .NET Revit 2019.1 SDK StairsAutomation sample.

Demonstrate how to remove warning messages for full on-line automation.

![StairsAutomation result](img/StairsAutomation_result.png)

- Stair #3 generates [8 warnings about overlapping handrail model line elements](warning_messages/StairsAutomation_warnings_stair_3_8.html).
- Stair #4 generates [1 warning about a missing riser](warning_messages/StairsAutomation_warnings_stair_4_1.html).

Happily, Revit warnings can easily be handled automatically making use of
the [Failure API](http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.32).

Specifically, we presented
a [generic warning swallower](http://thebuildingcoder.typepad.com/blog/2016/09/warning-swallower-and-roomedit3d-viewer-extension.html#2) that
can handle just about any warning message that crops up.

For the StairsAutomation sample, nothing much is required.

The code generating the stairs obviously runs inside a `Transaction`, and that, in turn, is enclosed in a `StairsEditScope`.

The call to `Commit` the stair editing scope is called with a custom failures preprocessor instance:

<pre class="code">
&nbsp;&nbsp;editScope.Commit(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">StairsEditScopeFailuresPreprocessor</span>()&nbsp;);
</pre>

In the original sample, the failures preprocessor does next to nothing:

<pre class="code">
<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">StairsEditScopeFailuresPreprocessor</span>&nbsp;
&nbsp;&nbsp;:&nbsp;<span style="color:#2b91af;">IFailuresPreprocessor</span>
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">FailureProcessingResult</span>&nbsp;PreprocessFailures(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FailuresAccessor</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">FailureProcessingResult</span>.Continue;
&nbsp;&nbsp;}
}
</pre>

I simply added the following lines of code to it, to delete all warnings before returning:
  
<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">FailureMessageAccessor</span>&gt;&nbsp;failures
&nbsp;&nbsp;=&nbsp;a.GetFailureMessages();
 
&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">FailureMessageAccessor</span>&nbsp;f&nbsp;<span style="color:blue;">in</span>&nbsp;failures&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FailureSeverity</span>&nbsp;fseverity&nbsp;=&nbsp;a.GetSeverity();
 
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;fseverity&nbsp;==&nbsp;<span style="color:#2b91af;">FailureSeverity</span>.Warning&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a.DeleteWarning(&nbsp;f&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

Now, all five stair variations are created without any warning messages being displayed.

For more deails, please refer to [The Building Coder](http://thebuildingcoder.typepad.com) discussion
on [swallowing StairsAutomation warnings](http://thebuildingcoder.typepad.com/blog/2018/09/swallowing-stairsautomation-warnings.html).


## Author

Jeremy Tammik,
[The Building Coder](http://thebuildingcoder.typepad.com),
[Forge](http://forge.autodesk.com) [Platform](https://developer.autodesk.com) [Development](https://autodesk-forge.github.io),
[ADN](http://www.autodesk.com/adn)
[Open](http://www.autodesk.com/adnopen),
[Autodesk Inc.](http://www.autodesk.com)
modified the original SDK sample.


## License

This sample is licensed under the terms of the [MIT License](http://opensource.org/licenses/MIT).
Please see the [LICENSE](LICENSE) file for full details.

