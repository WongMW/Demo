Type.registerNamespace("SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.CPDOnline.Designer");


SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.CPDOnline.Designer.CPDOnlineWidgetDesigner = function (element) {
    /* Initialize Message fields */
    this._topicCodes = null;
    this._hdnTopicCodes = null;
    this._productCount = null;
    /* Initialize the service url for the image thumbnails */


    /* Calls the base constructor */
    SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.CPDOnline.Designer.CPDOnlineWidgetDesigner.initializeBase(this, [element]);
}


SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.CPDOnline.Designer.CPDOnlineWidgetDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.CPDOnline.Designer.CPDOnlineWidgetDesigner.callBaseMethod(this, 'initialize');
        var self = this;

        jQuery(this.get_hdnTopicCodes()).change(function () {
            // update listbox
            var codes = $(this).val();
            if (codes)
                codes = codes.split(",");
            jQuery(self.get_topicCodes()).val(codes);
        });

        jQuery(this.get_topicCodes()).change(function () {
            // update hdn field
            var codes = $(this).val();
            if (codes)
                codes = codes.join();

            jQuery(self.get_hdnTopicCodes()).val(codes);
        });
    },
    dispose: function () {
        /* this is the place to unbind/dispose the event handlers created in the initialize method */
        SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.CPDOnline.Designer.CPDOnlineWidgetDesigner.callBaseMethod(this, 'dispose');

        jQuery(this.get_hdnTopicCodes()).unbind("change");
        jQuery(this.get_topicCodes()).unbind("change");
    },

    /* --------------------------------- public methods ---------------------------------- */

    findElement: function (id) {
        var result = jQuery(this.get_element()).find("#" + id).get(0);
        return result;
    },

    /* Called when the designer window gets opened and here is place to "bind" your designer to the control properties */
    refreshUI: function () {
        var controlData = this._propertyEditor.get_control(); /* JavaScript clone of your control - all the control properties will be properties of the controlData too */

        /* RefreshUI Message */
        jQuery(this.get_hdnTopicCodes()).val(controlData.TopicCodes);
        jQuery(this.get_productCount()).val(controlData.ProductCount);

        jQuery(this.get_hdnTopicCodes()).change();

        /* RefreshUI SelectedImageID */

    },

    /* Called when the "Save" button is clicked. Here you can transfer the settings from the designer to the control */
    applyChanges: function () {
        var controlData = this._propertyEditor.get_control();

        /* ApplyChanges Message */
        controlData.TopicCodes = jQuery(this.get_hdnTopicCodes()).val();
        controlData.ProductCount = jQuery(this.get_productCount()).val();

    },

    /* --------------------------------- event handlers ---------------------------------- */


    /* --------------------------------- private methods --------------------------------- */
    /* SelectedImageID private methods */

    /* --------------------------------- properties -------------------------------------- */

    /* Message properties */
    get_topicCodes: function () { return this._topicCodes; },
    set_topicCodes: function (value) { this._topicCodes = value; },

    get_hdnTopicCodes: function () { return this._hdnTopicCodes; },
    set_hdnTopicCodes: function (value) { this._hdnTopicCodes = value; },

    get_productCount: function () { return this._productCount; },
    set_productCount: function (value) { this._productCount = value; }
}

SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.CPDOnline.Designer.CPDOnlineWidgetDesigner.registerClass('SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.CPDOnline.Designer.CPDOnlineWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
