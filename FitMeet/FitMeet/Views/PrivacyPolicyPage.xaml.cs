
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrivacyPolicyPage : ContentPage
    {
        public PrivacyPolicyPage()
        {
            InitializeComponent();
            var htmlSource = new HtmlWebViewSource();
            var html = @"<p><strong>Personal Information </strong></p>
                        <p> </p><p>The Company collects and stores personal information about you including contact data, fitness data and demographic data. The Company may use this information to customise any content and advertisements you see on the Service including from third parties. Personal information may be stored on our developers&rsquo; secure servers located in Australia and as such this data and its&rsquo; security is covered by Australian law. </p>
                        <p> </p><p><strong>Fitness Data </strong></p><p> </p><p>Fitness data may include but is not limited to activity and exercise/sport preferences and skill levels, nutritional preferences, caloric intake and fitness goals. </p>
                        <p> </p><p><strong>Demographic Data </strong></p><p> </p><p>Demographic data may include but is not limited to gender, age, height, suburb, country and other information that does not specifically identify you.  </p>
                        <p> </p><p><strong>Sharing Information </strong></p><p> </p>
                        <p>You are not obligated to share data with other members of the Service but not doing so may prevent you from using certain features of the Service. Data may be shared with other members via your user account and profile, which you remain in control of while using the service. </p>
                        <p> </p><p><strong>Third Parties </strong></p><p> </p>
                        <p>Third parties may provide advertising via the Service that is customised based on the personal information collected by the Company. The Company will not provide your information to any third parties. The company will only enter in to agreements with reputable third parties to offer exclusive deals and discounts that are relevant and beneficial to the Company and its&rsquo; members. </p>
                        <p> </p><p><strong>User Analytics</strong> </p><p> </p>
                        <p>The Company may collect traffic data and monitor usage analytics (via cookies, web beacons and mobile device data) in order to customise content and advertising to its&rsquo; users. This data may also be used by the Company prior to releasing an update of the Service and the Company is likely to base new versions of the Service on this information. User analytics help the Company better understand who is using the Service and may be published from time to time in reports on usage and trends of the Service. The Company will use this data to better the Service based on user behaviour. </p>
                        <p> </p><p> </p><p> </p><p><strong>Web Beacons </strong></p><p> </p><p>Web beacons (also web bugs, pixel tags or clear gifs) are similar to cookies and are used to track users online movements or give access to cookies. Web beacons may be used to understand usage patterns, communicate with or deliver cookies or to count users visits to certain pages. </p>
                        <p> </p><p><strong>Cookies </strong></p><p> </p><p>A cookie is information that is provided to a website each time a user visits or submits a query to a website and is used to identify unique users of a website. Cookies are used to enhance the performance of the website based on the user. Cookie access can be disabled via your browser settings. By disabling cookie access this may affect certain features of the Service.  </p>
                        <p> </p><p><strong>Mobile device data</strong></p><p> </p><p>Mobile device data includes geo-location information (required for the function of the Service), carrier provider information, device type and identifier information. Only data that is relevant and affects the features of the Service is requested and may be stored on the Company&rsquo;s servers. </p>
                        <p> </p><p><strong>By law </strong></p><p> </p> 
                        <p>The Company will only provide personal information if required by law or in its sole discretion if the Company believes disclosure is necessary to protect the rights or the property of the Company. </p>
                        ";
            htmlSource.Html = @"<html><head><style> 
                                        p { word-break: break-word; white-space: normal; font-size: smaller;} 
                                </style> </head><body> "
                              + html +
                              "</body></html";
            webView.Source = htmlSource;
        }
    }
}