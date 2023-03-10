#pragma checksum "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Shared\_ReportLayout.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "646550dd747ce3f0e143e3b52e52052b4610a499"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__ReportLayout), @"mvc.1.0.view", @"/Views/Shared/_ReportLayout.cshtml")]
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
#nullable restore
#line 1 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Shared\_ReportLayout.cshtml"
using Microsoft.Extensions.Configuration;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"646550dd747ce3f0e143e3b52e52052b4610a499", @"/Views/Shared/_ReportLayout.cshtml")]
    #nullable restore
    public class Views_Shared__ReportLayout : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<!DOCTYPE html>\r\n<html>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "646550dd747ce3f0e143e3b52e52052b4610a4993000", async() => {
                WriteLiteral(@"
    <style>
        body {
            font-family: ""Courier New"", Courier, monospace;
            font-size: 13px;
        }

        body, h1, h2, h3, h4, h5, h6 {
            margin: 0px;
            padding: 0px;
        }

        small {
            font-size: small;
            color: #888;
        }

        #report-header {
            position: relative;
            border-bottom: 10px double #0066cc;
            background: #fafafa;
            padding: 10px;
        }

            #report-header table {
                margin: 0;
            }

            #report-header .sub-title {
                font-size: small;
                color: #888;
            }

            #report-header img {
                height: 50px;
                width: 50px;
            }

        #report-title {
            padding: 20px 0;
            border-bottom: 1px solid #ddd;
        }

        #report-body {
            padding: 0 20px;
            min-height: 500px;");
                WriteLiteral(@"
        }

        #report-footer {
            padding: 10px;
            background: #fafafa;
            border-top: 2px solid #0066cc;
            margin: 0 auto;
        }

            #report-footer table {
                margin: 0;
                overflow: hidden;
            }

        table,
        .table {
            width: 100%;
            max-width: 100%;
            margin-bottom: 1rem;
            border-collapse: collapse;
        }

            .table th,
            .table td {
                padding: 0.75rem;
                vertical-align: top;
                border-top: 1px solid #eceeef;
                text-align: left;
            }

            .table thead th {
                vertical-align: bottom;
                border-bottom: 2px solid #eceeef;
                text-align: left;
            }

            .table tbody + tbody {
                border-top: 2px solid #eceeef;
            }

            .table .table {
                ");
                WriteLiteral(@"background-color: #fff;
            }

        .table-sm th,
        .table-sm td {
            padding: 0.3rem;
        }

        .table-bordered {
            border: 1px solid #eceeef;
        }

            .table-bordered th,
            .table-bordered td {
                border: 1px solid #eceeef;
            }

            .table-bordered thead th,
            .table-bordered thead td {
                border-bottom-width: 2px;
            }

        .table-striped tbody tr:nth-of-type(odd) {
            background-color: rgba(0, 0, 0, 0.05);
        }
    </style>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "646550dd747ce3f0e143e3b52e52052b4610a4996693", async() => {
                WriteLiteral("\r\n    <div id=\"report-header\">\r\n        <table class=\"table table-sm\">\r\n            <tr>\r\n                <th align=\"left\" valign=\"middle\">\r\n                    <h3 class=\"company-name\">");
#nullable restore
#line 125 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Shared\_ReportLayout.cshtml"
                                        Write(Config["SITE_NAME"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h3>
                    <small class=""sub-title"">My Awesome App</small>
                </th>
                <th align=""right"" valign=""middle"" width=""auto"">
                    <div class=""company-info"">
                        <div>Phone: <span class=""sub-title"">+2335400000000</span></div>
                        <div>Email: <span class=""sub-title"">");
#nullable restore
#line 131 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Shared\_ReportLayout.cshtml"
                                                       Write(Config["SITE_NAME"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("@gmail.com</span></div>\r\n                    </div>\r\n                </th>\r\n            </tr>\r\n        </table>\r\n    </div>\r\n\r\n    <div id=\"report-body\">\r\n        ");
#nullable restore
#line 139 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Shared\_ReportLayout.cshtml"
   Write(RenderBody());

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
    </div>

    <div id=""report-footer"">
        <table class=""table table-sm"">
            <tr>
                <td align=""left"" valign=""middle"">
                    Footer content goes here.
                </td>
            </tr>
        </table>
    </div>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IConfiguration Config { get; private set; } = default!;
        #nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
