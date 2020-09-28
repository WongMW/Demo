Type.registerNamespace("SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.LatestNewsWidget.Designer");

SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.LatestNewsWidget.Designer.LatestNewsWidgetDesigner = function (element) {
    this._CategoriesSelector = null;
    this._limit = null;

    /* Initialize SelectedPageID fields */
    this._pageSelectorSelectedPageID = null;
    this._selectorTagSelectedPageID = null;
    this._SelectedPageIDDialog = null;
    this._imageSize = null;

    this._showPageSelectorSelectedPageIDDelegate = null;
    this._pageSelectedSelectedPageIDDelegate = null;
    
    /* Calls the base constructor */
    SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.LatestNewsWidget.Designer.LatestNewsWidgetDesigner.initializeBase(this, [element]);
}

SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.LatestNewsWidget.Designer.LatestNewsWidgetDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.LatestNewsWidget.Designer.LatestNewsWidgetDesigner.callBaseMethod(this, 'initialize');

        /* Initialize SelectedPageID */
        this._showPageSelectorSelectedPageIDDelegate = Function.createDelegate(this, this._showPageSelectorSelectedPageIDHandler);
        $addHandler(this.get_pageSelectButtonSelectedPageID(), "click", this._showPageSelectorSelectedPageIDDelegate);

        this._pageSelectedSelectedPageIDDelegate = Function.createDelegate(this, this._pageSelectedSelectedPageIDHandler);
        this.get_pageSelectorSelectedPageID().add_doneClientSelection(this._pageSelectedSelectedPageIDDelegate);

        if (this._selectorTagSelectedPageID) {
            this._SelectedPageIDDialog = jQuery(this._selectorTagSelectedPageID).dialog({
                autoOpen: false,
                modal: false,
                width: 395,
                closeOnEscape: true,
                resizable: false,
                draggable: false,
                zIndex: 5000
            });
        }
    },
    dispose: function () {
        /* this is the place to unbind/dispose the event handlers created in the initialize method */
        SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.LatestNewsWidget.Designer.LatestNewsWidgetDesigner.callBaseMethod(this, 'dispose');

        /* Dispose SelectedPageID */
        if (this._showPageSelectorSelectedPageIDDelegate) {
            $removeHandler(this.get_pageSelectButtonSelectedPageID(), "click", this._showPageSelectorSelectedPageIDDelegate);
            delete this._showPageSelectorSelectedPageIDDelegate;
        }

        if (this._pageSelectedSelectedPageIDDelegate) {
            this.get_pageSelectorSelectedPageID().remove_doneClientSelection(this._pageSelectedSelectedPageIDDelegate);
            delete this._pageSelectedSelectedPageIDDelegate;
        }
    },

    /* --------------------------------- public methods ---------------------------------- */

    findElement: function (id) {
        var result = jQuery(this.get_element()).find("#" + id).get(0);
        return result;
    },

    /* Called when the designer window gets opened and here is place to "bind" your designer to the control properties */
    refreshUI: function () {
        var data = this._propertyEditor.get_control();

        jQuery(this.get_limit()).val(data.Limit);
        jQuery(this.get_imageSize()).val(data.ImageSize);
        
        // load categories
        var c = this.get_CategoriesSelector();
        var cats = data.CategoryValue;
        if (cats != null)
            c.set_value(data.CategoryValue.split(","));


        /* RefreshUI SelectedPageID */
        if (data.SelectedPageID && data.SelectedPageID !== "00000000-0000-0000-0000-000000000000") {
            var pagesSelectorSelectedPageID = this.get_pageSelectorSelectedPageID().get_pageSelector();
            var selectedPageLabelSelectedPageID = this.get_selectedSelectedPageIDLabel();
            var selectedPageButtonSelectedPageID = this.get_pageSelectButtonSelectedPageID();
            pagesSelectorSelectedPageID.add_selectionApplied(function (o, args) {
                var selectedPage = pagesSelectorSelectedPageID.get_selectedItem();
                if (selectedPage) {
                    selectedPageLabelSelectedPageID.innerHTML = selectedPage.Title.Value;
                    jQuery(selectedPageLabelSelectedPageID).show();
                    selectedPageButtonSelectedPageID.innerHTML = '<span>Change</span>';
                }
            });
            pagesSelectorSelectedPageID.set_selectedItems([{ Id: data.SelectedPageID }]);
        }
    },

    /* Called when the "Save" button is clicked. Here you can transfer the settings from the designer to the control */
    applyChanges: function () {
        var controlData = this._propertyEditor.get_control();

        controlData.Limit = jQuery(this.get_limit()).val();
        controlData.ImageSize = jQuery(this.get_imageSize()).val();

        // save selected categories
        var c = this.get_CategoriesSelector();
        var cats = c.get_value();
        if (cats != null)
            controlData.CategoryValue = c.get_value().join();
    
    },

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */
    /* SelectedPageID private methods */
    _showPageSelectorSelectedPageIDHandler: function (selectedItem) {
        var controlData = this._propertyEditor.get_control();
        var pagesSelector = this.get_pageSelectorSelectedPageID().get_pageSelector();
        if (controlData.SelectedPageID) {
            pagesSelector.set_selectedItems([{ Id: controlData.SelectedPageID }]);
        }
        this._SelectedPageIDDialog.dialog("open");
        jQuery("#designerLayoutRoot").hide();
        this._SelectedPageIDDialog.dialog().parent().css("min-width", "355px").css("top", "-160px");
        dialogBase.resizeToContent();
    },

    _pageSelectedSelectedPageIDHandler: function (items) {
        var controlData = this._propertyEditor.get_control();
        var pagesSelector = this.get_pageSelectorSelectedPageID().get_pageSelector();
        this._SelectedPageIDDialog.dialog("close");
        jQuery("#designerLayoutRoot").show();
        dialogBase.resizeToContent();
        if (items == null) {
            return;
        }
        var selectedPage = pagesSelector.get_selectedItem();
        if (selectedPage) {
            this.get_selectedSelectedPageIDLabel().innerHTML = selectedPage.Title.Value;
            jQuery(this.get_selectedSelectedPageIDLabel()).show();
            this.get_pageSelectButtonSelectedPageID().innerHTML = '<span>Change</span>';
            controlData.SelectedPageID = selectedPage.Id;
        }
        else {
            jQuery(this.get_selectedSelectedPageIDLabel()).hide();
            this.get_pageSelectButtonSelectedPageID().innerHTML = '<span>Select...</span>';
            controlData.SelectedPageID = "00000000-0000-0000-0000-000000000000";
        }
    },

    /* --------------------------------- properties -------------------------------------- */

   
    // Categories Selector
    get_CategoriesSelector: function () {
        return this._CategoriesSelector;
    },
    set_CategoriesSelector: function (value) {
        this._CategoriesSelector = value;
    },

    get_limit: function () { return this._limit; },
    set_limit: function (value) { this._limit = value; },

    /* SelectedPageID properties */
    get_pageSelectButtonSelectedPageID: function () {
        if (this._pageSelectButtonSelectedPageID == null) {
            this._pageSelectButtonSelectedPageID = this.findElement("pageSelectButtonSelectedPageID");
        }
        return this._pageSelectButtonSelectedPageID;
    },
    get_selectedSelectedPageIDLabel: function () {
        if (this._selectedSelectedPageIDLabel == null) {
            this._selectedSelectedPageIDLabel = this.findElement("selectedSelectedPageIDLabel");
        }
        return this._selectedSelectedPageIDLabel;
    },
    get_pageSelectorSelectedPageID: function () {
        return this._pageSelectorSelectedPageID;
    },
    set_pageSelectorSelectedPageID: function (val) {
        this._pageSelectorSelectedPageID = val;
    },
    get_selectorTagSelectedPageID: function () {
        return this._selectorTagSelectedPageID;
    },
    set_selectorTagSelectedPageID: function (value) {
        this._selectorTagSelectedPageID = value;
    },

    get_imageSize: function () { return this._imageSize; },
    set_imageSize: function (value) { this._imageSize = value; }

}

SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.LatestNewsWidget.Designer.LatestNewsWidgetDesigner.registerClass('SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.LatestNewsWidget.Designer.LatestNewsWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
