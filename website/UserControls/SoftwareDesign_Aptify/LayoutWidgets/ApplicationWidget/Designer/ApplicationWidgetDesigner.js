Type.registerNamespace("SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.ApplicationWidget.Designer");

SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.ApplicationWidget.Designer.ApplicationWidgetDesigner = function (element) {
    /* Initialize fields */
    this._appOneTitle = null;
    this._appOneDate = null;
    this._appTwoTitle = null;
    this._appTwoDate = null;
    this._appThreeTitle = null;
    this._appThreeDate = null;

    /* Calls the base constructor */
    SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.ApplicationWidget.Designer.ApplicationWidgetDesigner.initializeBase(this, [element]);
}

SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.ApplicationWidget.Designer.ApplicationWidgetDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.ApplicationWidget.Designer.ApplicationWidgetDesigner.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        /* this is the place to unbind/dispose the event handlers created in the initialize method */
        SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.ApplicationWidget.Designer.ApplicationWidgetDesigner.callBaseMethod(this, 'dispose');
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
        jQuery(this.get_appOneTitle()).val(controlData.AppOneTitle);
        jQuery(this.get_appOneDate()).val(controlData.AppOneDate);
        jQuery(this.get_appTwoTitle()).val(controlData.AppTwoTitle);
        jQuery(this.get_appTwoDate()).val(controlData.AppTwoDate);
        jQuery(this.get_appThreeTitle()).val(controlData.AppThreeTitle);
        jQuery(this.get_appThreeDate()).val(controlData.AppThreeDate);
    },

    /* Called when the "Save" button is clicked. Here you can transfer the settings from the designer to the control */
    applyChanges: function () {
        var controlData = this._propertyEditor.get_control();

        /* ApplyChanges */
        controlData.AppOneTitle = jQuery(this.get_appOneTitle()).val();
        controlData.AppOneDate = jQuery(this.get_appOneDate()).val();
        controlData.AppTwoTitle = jQuery(this.get_appTwoTitle()).val();
        controlData.AppTwoDate = jQuery(this.get_appTwoDate()).val();
        controlData.AppThreeTitle = jQuery(this.get_appThreeTitle()).val();
        controlData.AppThreeDate = jQuery(this.get_appThreeDate()).val();
    },

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */

    /* properties */
    get_appOneTitle: function () { return this._appOneTitle; },
    set_appOneTitle: function (value) { this._appOneTitle = value; },
    get_appOneDate: function () { return this._appOneDate; },
    set_appOneDate: function (value) { this._appOneDate = value; },
    get_appTwoTitle: function () { return this._appTwoTitle; },
    set_appTwoTitle: function (value) { this._appTwoTitle = value; },
    get_appTwoDate: function () { return this._appTwoDate; },
    set_appTwoDate: function (value) { this._appTwoDate = value; },
    get_appThreeTitle: function () { return this._appThreeTitle; },
    set_appThreeTitle: function (value) { this._appThreeTitle = value; },
    get_appThreeDate: function () { return this._appThreeDate; },
    set_appThreeDate: function (value) { this._appThreeDate = value; }
}

SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.ApplicationWidget.Designer.ApplicationWidgetDesigner.registerClass('SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.ApplicationWidget.Designer.ApplicationWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
