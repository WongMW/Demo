<%@ Control Language="C#" %>

<!-- Bootstrap CSS -->
<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
<!-- BootstrapValidator CSS -->
<link rel="stylesheet" href="//cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/css/bootstrapValidator.min.css" />
<!--  intl-tel-input CSS -->
<link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/intl-tel-input/12.1.13/css/intlTelInput.css" />


<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/12.1.13/js/intlTelInput.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/12.1.13/js/utils.js"></script>
<!-- jQuery and Bootstrap JS -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
<%-- <script src="https://cdnjs.cloudflare.com/ajax/libs/1000hz-bootstrap-validator/0.11.5/validator.min.js"></script>--%>
<!-- BootstrapValidator JS -->
<script type="text/javascript" src="//cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/js/bootstrapValidator.min.js"></script>

<style>
    <!-- ADDING STYLES OVERWRITTEN BY BOOTSTRAP STYLES -->
    body, .office-link {
        font-size: 16px !important;
        line-height: 1.4em;
    }

    .title-holder {
        padding: 10px 0px;
        margin-top: 14px;
    }

    h1, h2, #Tabs > ul.nav-tabs > li > a {
        font-family: 'Source Sans Pro', sans-serif;
        line-height: 1.4em;
        font-weight: 700;
        color: #003D51;
    }

    h1 {
        font-size: 32px;
    }

    h2, #Tabs > ul.nav-tabs > li > a {
        font-size: 24px;
    }

    a, a:link, a:visited, a:hover, a:active {
        color: #003D51;
    }

    .main-nav li a, .link-holder a, .sfNavHorizontalDropDown .k-item > a.k-link, .sfNavHorizontal li.loginli a {
        font-size: 16px;
    }

        .main-nav li a:hover, .link-holder a:hover, .sfNavHorizontal li.loginli a:hover {
            text-decoration: none !important;
        }

    .link-holder a, .sfNavHorizontal li.loginli a {
        color: #fff;
    }

    .footer-wrapper-main li, .footer-wrapper-main li > a, .footer-wrapper-main p, .tel-number {
        line-height: 1.4em;
    }

    .sfnewsletterForm.sfSubscribe ol.sfnewsletterFieldsList li.sfnewsletterField input, .dataTables_filter input[type=search] {
        font-weight: normal;
    }

    .sfnewsletterForm.sfSubscribe ol.sfnewsletterFieldsList {
        margin-bottom: 0px;
    }

    .footer-wrapper-main h2, .footer-wrapper-main li > a, .office-link {
        font-family: 'Source Sans Pro', sans-serif;
    }

    .footer-navs .link-holder a {
        color: #8C1D40;
        font-size: 12px;
    }

    .nav-tabs > li > a {
        background-color: #eee;
    }

    .nav-tabs > li {
        float: left;
        margin-bottom: -1px;
    }

    .main-title-banner > h2 {
        margin: 0pc !important;
    }

    .sfContentViews.sfSingleContentView {
        text-align: left;
    }

    .col-sm-10:not(.has-error) .form-control-feedback,
    .col-sm-10:not(.has-error) .help-block,
    .col-xs-4:not(.has-error) .form-control-feedback,
    .col-xs-4:not(.has-error) .help-block {
        display: none;
    }

    .gclid_field {
        display: none;
    }
</style>

<script type="text/javascript">
    $(function () {
        $(document).on("keypress", "input", function () {
            if ($(this).closest(".has-error").length) $(this).closest(".has-error").removeClass("has-error");
        });
    });
</script>

<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div id="designerLayoutRoot" class="sfContentViews sfSingleContentView">
            <div id="block" runat="server" class="button-block">
                <asp:Button ID="PageLink" runat="server" Text="This is a sample button" class="btn-full-width btn"></asp:Button>
                <asp:Panel ID="pnlThankYou" runat="server" Visible="false">
                    <div class="container-fluid bootstrap-style">
                        <div class="row-fluid sfContentBlock" style="margin-top: 20px;">
                            <fieldset>
                                <%--<legend class="text-center" style="color:#861F41">Thank you</legend>--%>
                                <h2>Thank you</h2>
                                <p>Your form was submitted succesfully. Please click on the button below to download your PDF.</p>
                                <asp:HyperLink Target="_blank" runat="server" ID="linkPdfButton" Text="Proceed to Download" CssClass="cai-btn cai-btn-red"></asp:HyperLink>
                            </fieldset>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlFormToFill" runat="server" Visible="false" CssClass="pnlFormToFill">
                    <div class="container-fluid bootstrap-style">
                        <div class="row-fluid" style="margin-top: 20px;">
                            <fieldset>
                                <legend class="text-center" style="color: #861F41">Fill in the form</legend>
                                <p>Please tell us a little about yourself by filling out the form below. Once you click submit you will be presented with the download link.</p>

                                <p style="color: #861F41" runat="server" id="lblErrorMessage" visible="false"></p>

                                <!--  First Name -->
                                <div class="form-group row">
                                    <asp:TextBox ID="gclid_field" runat="server" name="gclid_field" class="gclid_field" value=""></asp:TextBox>
                                    <%--<input runat="server" type="hidden" id="gclid_field" name="gclid_field" value=""><!-- WongS, Added as part of #21182 -->--%>
                                    <label class="col-sm-2 col-form-label required" for="txtFirstName">First Name</label>
                                    <div class="col-sm-10" runat="server" id="holderTxtFirstName">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                            <asp:TextBox ID="txtFirstName" runat="server" class="form-control txtFirstName" placeholder="First Name" name="txtFirstName" onchange="addGclid(this)"></asp:TextBox>
                                        </div>
                                        <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                                        <small id="lblFirstNameError" runat="server" class="help-block"></small>
                                    </div>
                                </div>
                                <!--  Last Name -->
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label required" for="txtLastName">Last Name</label>
                                    <div class="col-sm-10" runat="server" id="holderTxtLastName">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                            <asp:TextBox ID="txtLastName" runat="server" class="form-control" placeholder="Last Name" name="txtLastName"></asp:TextBox>
                                        </div>
                                        <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                                        <small id="lblLastNameError" runat="server" class="help-block"></small>
                                    </div>
                                </div>

                                <!--  Email -->
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label required" for="txtEmail">Email</label>
                                    <div class="col-sm-10" runat="server" id="holderTxtEmail">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="glyphicon glyphicon-envelope"></i></span>
                                            <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Email" name="txtEmail"></asp:TextBox>
                                        </div>
                                        <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                                        <small id="lblEmailError" runat="server" class="help-block"></small>
                                    </div>
                                </div>
                                <!--  Contact Number -->
                                <div class="form-group row">
                                    <label class="col-sm-2 col-form-label required" for="countyrcode">Contact Number</label>
                                    <div class="">
                                        <div class="col-xs-4 col-sm-3" runat="server" id="holderTxtCountryCode">
                                            <asp:TextBox ID="countyrcode" runat="server" class="form-control" placeholder="Country code" name="countrycode"></asp:TextBox>
                                            <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                                            <span class="txtbox-help-text">E.g. 353 (for ROI) | 44 (for UK)</span>
                                            <small id="lblCountryCodeError" runat="server" class="help-block"></small>
                                        </div>
                                        <div class="col-xs-4 col-sm-3" runat="server" id="holderTxtMobileArea">
                                            <asp:TextBox ID="mobilearea" runat="server" class="form-control" placeholder="Mobile\area code" name="mobilearea"></asp:TextBox>
                                            <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                                            <small id="lblMobileAreaError" runat="server" class="help-block"></small>
                                        </div>
                                        <div class="col-xs-4 col-sm-4" runat="server" id="holderTxtNumber">
                                            <asp:TextBox ID="number" runat="server" class="form-control" placeholder="Phone number" name="number"></asp:TextBox>
                                            <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                                            <small id="lblNumberError" runat="server" class="help-block"></small>
                                        </div>
                                    </div>
                                </div>								
                                <!-- PATCH FIX CHECKBOXES #21366 -->
                                <div class="form-group row">
                                    <div class="col-sm-2"></div>
									<asp:CheckBox ID="chkAllowCommunication" CssClass="checkbox-inline col-xs-12 col-sm-10" runat="server" 
									Text="Please tick this box if you'd like to receive communications from us about Professional Development qualifications" ></asp:CheckBox>
                                </div>

                                <div class="form-group row">
                                    <div class="text-right">
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="submitBtn" OnClientClick="addGclid(this)" />
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="PageLink" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
<script type="text/javascript">
    $(function () {
        $(document).on("keypress", "input", function () {
            if ($(this).closest(".has-error").length) $(this).closest(".has-error").removeClass("has-error");
        });
    });

    function getParam(p) {
        let match = RegExp('[?&]' + p + '=([^&]*)').exec(window.location.search);
        return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
    }

    function addToStorage(key, value) {
        let expiryPeriod = 90 * 24 * 60 * 60 * 1000; // 90 day expiry in milliseconds
        let expiryDate = new Date().getTime() + expiryPeriod;
        let record = { value: value, expiryDate: expiryDate };
        localStorage.setItem(key, JSON.stringify(record));
    }

    function storeGclid() {
        let gclidParam = getParam('gclid');

        if (gclidParam) {
            addToStorage('gclid', gclidParam);
        }
    }

    function addGclid(thiss) {
        storeGclid(); // store gclid param to localstorage
        let gclidFormField = document.getElementById(thiss.id.replace('txtFirstName', 'gclid_field').replace('btnSave', 'gclid_field'));
        let currDate = new Date().getTime();
        let gclsrcParam = getParam('gclsrc');
        let isGclsrcValid = !gclsrcParam || gclsrcParam.indexOf('aw') !== -1;
        let gclid = JSON.parse(localStorage.getItem('gclid'));
        let isGclidValid = gclid && currDate < gclid.expiryDate;

        if (gclidFormField && isGclidValid && isGclsrcValid) {
            gclidFormField.value = gclid.value;
        }
    }
</script>
