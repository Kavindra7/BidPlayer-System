#pragma checksum "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Users\ExportList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7b85a0a2e3895c1dbf6614eff9bf3704c50eec9c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Users_ExportList), @"mvc.1.0.view", @"/Views/Users/ExportList.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7b85a0a2e3895c1dbf6614eff9bf3704c50eec9c", @"/Views/Users/ExportList.cshtml")]
    #nullable restore
    public class Views_Users_ExportList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<ASPRad.Models.Users>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Users\ExportList.cshtml"
      
    Layout = "_ReportLayout";
    

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div id=""report-title""><h1></h1></div>
    <table class=""table table-sm table-striped"">
        <thead>
            <tr>
                <th>Id</th>
                <th>Full Name</th>
                <th>Username</th>
                <th>Email</th>
                <th>Age</th>
                <th>Gender</th>
                <th>Balance</th>
                <th>User Role</th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 21 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Users\ExportList.cshtml"
             foreach(var record in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 24 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Users\ExportList.cshtml"
               Write(record.id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 25 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Users\ExportList.cshtml"
               Write(record.full_name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 26 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Users\ExportList.cshtml"
               Write(record.username);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 27 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Users\ExportList.cshtml"
               Write(record.email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 28 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Users\ExportList.cshtml"
               Write(record.age);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 29 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Users\ExportList.cshtml"
               Write(record.gender);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 30 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Users\ExportList.cshtml"
               Write(record.balance);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 31 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Users\ExportList.cshtml"
               Write(record.user_role_id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 33 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Users\ExportList.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            \r\n        </tbody>\r\n    </table>\r\n    \r\n    ");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<ASPRad.Models.Users>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591