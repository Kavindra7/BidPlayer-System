#pragma checksum "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Auth\VerifyEmailTemplate.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "291dc12d677bf2667f319335b821263440b40aa8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Auth_VerifyEmailTemplate), @"mvc.1.0.view", @"/Views/Auth/VerifyEmailTemplate.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"291dc12d677bf2667f319335b821263440b40aa8", @"/Views/Auth/VerifyEmailTemplate.cshtml")]
    #nullable restore
    public class Views_Auth_VerifyEmailTemplate : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "291dc12d677bf2667f319335b821263440b40aa82817", async() => {
                WriteLiteral(@"
    <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
    <meta name=""viewport"" content=""width=device-width""/>

    <style type=""text/css"">
		* {
			margin: 0;
			padding: 0;
			font-size: 100%;
			font-family: 'Avenir Next', ""Helvetica Neue"", ""Helvetica"", Helvetica, Arial, sans-serif;
			line-height: 1.65;
		}
		img {
			max-width: 100%;
			margin: 0 auto;
			display: block;
		}
		body,
		.body-wrap {
			width: 100% !important;
			height: 100%;
			background: #efefef;
			-webkit-font-smoothing: antialiased;
			-webkit-text-size-adjust: none;
		}
		a {
			text-decoration: none;
		}
		.text-center {
			text-align: center;
		}
		.text-right {
			text-align: right;
		}
		.text-left {
			text-align: left;
		}
		.button {
			display: inline-block;
			color: white;
			background: #71bc37;
			padding: 5px 10px;
			font-weight: bold;
			border-radius: 4px;
		}
		h1,
		h2,
		h3,
		h4,
		h5,
		h6 {
			margin-bottom: 20px;
			line-height: 1.25;
		");
                WriteLiteral(@"}
		h1 {
			font-size: 32px;
		}
		h2 {
			font-size: 28px;
		}
		h3 {
			font-size: 24px;
		}
		h4 {
			font-size: 20px;
		}
		h5 {
			font-size: 16px;
		}
		p,
		ul,
		ol {
			font-size: 16px;
			font-weight: normal;
			margin-bottom: 20px;
		}
		.container {
			display: block !important;
			clear: both !important;
			margin: 0 auto !important;
			max-width: 580px !important;
		}
		.container table {
			width: 100% !important;
			border-collapse: collapse;
		}
		.container .masthead {
			padding: 50px 0;
			background: #2c3e50;
			color: white;
		}
		.container .masthead h1 {
			margin: 0 auto !important;
			max-width: 90%;
			text-transform: uppercase;
		}
		.container .content {
			background: white;
			padding: 30px 35px;
		}
		.container .content.footer {
			background: none;
		}
		.container .content.footer p {
			margin-bottom: 0;
			color: #888;
			text-align: center;
			font-size: 14px;
		}
		.container .content.footer a {
			color: #888;
			");
                WriteLiteral(@"text-decoration: none;
			font-weight: bold;
		}
		.alert {
			display: inline-block;
			margin: 20px auto;
			background: #fff;
			color: #EB2D30;
			padding: 10px 20px;
			border-radius: 4px;
		}
		.button.fb {
			background: #0066aa;
		}
		.button.tw {
			background: #4099FF;
		}
		.button.yt {
			background: #EB2D30;
		}
		.button-action {
			background: #2c3e50;
			display: block;
			color: white;
			padding: 10px;
			font-size: 20px;
			width: 200px;
			margin: 5px auto;
			font-weight: bold;
			border-radius: 4px;
			text-align: center;
			text-shadow: 0 1px 1px rgba(0, 0, 0, 0.06);
		}
		.button-action:hover{
			background: #71bc37;
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "291dc12d677bf2667f319335b821263440b40aa86621", async() => {
                WriteLiteral(@"

<table class=""body-wrap"">
    <tr>
        <td class=""container"">
            <table>
                <tr>
                    <td align=""center"" class=""masthead"">
                        <h1>Email Verification</h1>
                    </td>
                </tr>
                <tr>
                    <td class=""content"">
                        <h2>Hi ");
#nullable restore
#line 167 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Auth\VerifyEmailTemplate.cshtml"
                          Write(ViewBag.username);

#line default
#line hidden
#nullable disable
                WriteLiteral(",</h2>\r\n\t\t\t\t\t\t<p>Welcome to <strong>");
#nullable restore
#line 168 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Auth\VerifyEmailTemplate.cshtml"
                                         Write(ViewBag.sitename);

#line default
#line hidden
#nullable disable
                WriteLiteral("</strong> !</p>\r\n\t\t\t\t\t\t<p>Please verify your email by clicking on the verify button.</p>\r\n\t\t\t\t\t\t<a");
                BeginWriteAttribute("href", " href=\"", 3343, "\"", 3369, 1);
#nullable restore
#line 170 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Auth\VerifyEmailTemplate.cshtml"
WriteAttributeValue("", 3350, ViewBag.verifylink, 3350, 19, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"button-action\">Verify Email</a>\r\n\t\t\t\t\t\t<br />\r\n                        <div>Sent by ");
#nullable restore
#line 172 "C:\Users\Kavindra\Desktop\BidPlayersDB\WebAPI\Views\Auth\VerifyEmailTemplate.cshtml"
                                Write(ViewBag.sitename);

#line default
#line hidden
#nullable disable
                WriteLiteral(" Support Team</div>\r\n                    </td>\r\n                </tr>\r\n            </table>\r\n        </td>\r\n    </tr>\r\n\r\n</table>\r\n");
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