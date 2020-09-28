Type.registerNamespace("SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.Designer");

SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.Designer.GDPRModalDesigner = function (element) {
    /* Initialize fields */
    this._monthsToHideAfterApproval = null;

    /* Calls the base constructor */
    SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.Designer.GDPRModalDesigner.initializeBase(this, [element]);
}

SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.Designer.GDPRModalDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.Designer.GDPRModalDesigner.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        /* this is the place to unbind/dispose the event handlers created in the initialize method */
        SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.Designer.GDPRModalDesigner.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods ---------------------------------- */

    findElement: function (id) {
        var result = jQuery(this.get_element()).find("#" + id).get(0);
        return result;
    },

    /* Called when the designer window gets opened and here is place to "bind" your designer to the control properties */
    refreshUI: function () {
        var controlData = this._propertyEditor.get_control(); /* JavaScript clone of your control - all the control properties will be properties of the controlData too */

        /* RefreshUI  */
        jQuery(this.get_monthsToHideAfterApproval()).val(controlData.MonthsToHideAfterApproval);
    },

    /* Called when the "Save" button is clicked. Here you can transfer the settings from the designer to the control */
    applyChanges: function () {
        var controlData = this._propertyEditor.get_control();

        /* ApplyChanges */
        controlData.MonthsToHideAfterApproval = jQuery(this.get_monthsToHideAfterApproval()).val();
    },

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */

    /* properties */
    get_monthsToHideAfterApproval: function () { return this._monthsToHideAfterApproval; },
    set_monthsToHideAfterApproval: function (value) { this._monthsToHideAfterApproval = value; }
}

SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.Designer.GDPRModalDesigner.registerClass('SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.Designer.GDPRModalDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
