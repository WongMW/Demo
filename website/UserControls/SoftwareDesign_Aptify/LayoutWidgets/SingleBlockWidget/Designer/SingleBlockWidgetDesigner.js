Type.registerNamespace("SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockWidget.Designer");

SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockWidget.Designer.SingleBlockWidgetDesigner = function (element) {
    /* Initialize Message fields */
    this._message = null;
    this._buttonlabel = null;
    this._linkText = null;
    this._title = null;
    this._style = null;
    this._imageSize = null;

    /* Initialize SelectedPageID fields */
    this._pageSelectorSelectedPageID = null;
    this._selectorTagSelectedPageID = null;
    this._SelectedPageIDDialog = null;

    this._showPageSelectorSelectedPageIDDelegate = null;
    this._pageSelectedSelectedPageIDDelegate = null;

    /* Initialize the service url for the image thumbnails */
    this.imageServiceUrl = null;

    
    /* Calls the base constructor */
    SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockWidget.Designer.SingleBlockWidgetDesigner.initializeBase(this, [element]);
}

SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockWidget.Designer.SingleBlockWidgetDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockWidget.Designer.SingleBlockWidgetDesigner.callBaseMethod(this, 'initialize');

        /* Initialize SelectedImageID */
        this._selectButtonSelectedImageIDClickDelegate = Function.createDelegate(this, this._selectButtonSelectedImageIDClicked);
        if (this._selectButtonSelectedImageID) {
            $addHandler(this._selectButtonSelectedImageID, "click", this._selectButtonSelectedImageIDClickDelegate);
        }

        this._deselectButtonSelectedImageIDClickDelegate = Function.createDelegate(this, this._deselectButtonSelectedImageIDClicked);
        if (this._deselectButtonSelectedImageID) {
            $addHandler(this._deselectButtonSelectedImageID, "click", this._deselectButtonSelectedImageIDClickDelegate);
        }

        if (this._selectorSelectedImageID) {
            this._SelectedImageIDDialog = jQuery(this._selectorSelectedImageID.get_element()).dialog({
                autoOpen: false,
                modal: false,
                width: 655,
                height: "auto",
                closeOnEscape: true,
                resizable: false,
                draggable: false,
                zIndex: 5000,
                close: this._selectorSelectedImageIDCloseDelegate
            });
        }

        jQuery("#previewSelectedImageID").load(function () {
            dialogBase.resizeToContent();
        });

        this._selectorSelectedImageIDInsertDelegate = Function.createDelegate(this, this._selectorSelectedImageIDInsertHandler);
        this._selectorSelectedImageID.set_customInsertDelegate(this._selectorSelectedImageIDInsertDelegate);
        $addHandler(this._selectorSelectedImageID._cancelLink, "click", this._selectorSelectedImageIDCloseHandler);
        this._selectorSelectedImageIDCloseDelegate = Function.createDelegate(this, this._selectorSelectedImageIDCloseHandler);
        this._selectorSelectedImageIDUploaderViewFileChangedDelegate = Function.createDelegate(this, this._selectorSelectedImageIDUploaderViewFileChangedHandler);

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
        SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockWidget.Designer.SingleBlockWidgetDesigner.callBaseMethod(this, 'dispose');

        /* Dispose SelectedImageID */
        if (this._selectButtonSelectedImageID) {
            $removeHandler(this._selectButtonSelectedImageID, "click", this._selectButtonSelectedImageIDClickDelegate);
        }
        if (this._selectButtonSelectedImageIDClickDelegate) {
            delete this._selectButtonSelectedImageIDClickDelegate;
        }

        if (this._deselectButtonSelectedImageID) {
            $removeHandler(this._deselectButtonSelectedImageID, "click", this._deselectButtonSelectedImageIDClickDelegate);
        }
        if (this._deselectButtonSelectedImageIDClickDelegate) {
            delete this._deselectButtonSelectedImageIDClickDelegate;
        }

        $removeHandler(this._selectorSelectedImageID._cancelLink, "click", this._selectorSelectedImageIDCloseHandler);

        if (this._selectorSelectedImageIDCloseDelegate) {
            delete this._selectorSelectedImageIDCloseDelegate;
        }

        if (this._selectorSelectedImageIDUploaderViewFileChangedDelegate) {
            this._selectorSelectedImageID._uploaderView.remove_onFileChanged(this._selectorSelectedImageIDUploaderViewFileChangedDelegate);
            delete this._selectorSelectedImageIDUploaderViewFileChangedDelegate;
        }

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
        var controlData = this._propertyEditor.get_control(); /* JavaScript clone of your control - all the control properties will be properties of the controlData too */

        /* RefreshUI Message */
        jQuery(this.get_message()).val(controlData.Message);
        jQuery(this.get_buttonlabel()).val(controlData.ButtonLabel);
        jQuery(this.get_title()).val(controlData.Title);
        jQuery(this.get_style()).val(controlData.Style);
        jQuery(this.get_linkText()).val(controlData.LinkText);
        jQuery(this.get_imageSize()).val(controlData.ImageSize);

        /* RefreshUI SelectedImageID */
        this.get_selectedSelectedImageID().innerHTML = controlData.SelectedImageID;
        if (controlData.SelectedImageID && controlData.SelectedImageID != "00000000-0000-0000-0000-000000000000") {
            this.get_selectButtonSelectedImageID().innerHTML = "<span class=\"sfLinkBtnIn\">Change</span>";
            jQuery(this.get_deselectButtonSelectedImageID()).show()
            var url = this.imageServiceUrl + controlData.SelectedImageID + "/?published=true";
            jQuery.ajax({
                url: url,
                type: "GET",
                contentType: "application/json",
                dataType: "json",
                success: function (data) {
                    jQuery("#previewSelectedImageID").show();
                    jQuery("#previewSelectedImageID").attr("src", data.Item.ThumbnailUrl);
                    dialogBase.resizeToContent();
                }
            });
        }
        else {
            jQuery(this.get_deselectButtonSelectedImageID()).hide()
        }

        /* RefreshUI SelectedPageID */
        if (controlData.SelectedPageID && controlData.SelectedPageID !== "00000000-0000-0000-0000-000000000000") {
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
            pagesSelectorSelectedPageID.set_selectedItems([{ Id: controlData.SelectedPageID }]);
        }
    },

    /* Called when the "Save" button is clicked. Here you can transfer the settings from the designer to the control */
    applyChanges: function () {
        var controlData = this._propertyEditor.get_control();

        /* ApplyChanges Message */
        controlData.Message = jQuery(this.get_message()).val();
        controlData.ButtonLabel = jQuery(this.get_buttonlabel()).val();
        controlData.Title = jQuery(this.get_title()).val();
        controlData.Style = jQuery(this.get_style()).val();
        controlData.LinkText = jQuery(this.get_linkText()).val();
        controlData.ImageSize = jQuery(this.get_imageSize()).val();

        /* ApplyChanges SelectedImageID */
        controlData.SelectedImageID = this.get_selectedSelectedImageID().innerHTML;
    },

    /* --------------------------------- event handlers ---------------------------------- */
    /* SelectedImageID event handlers */
    _selectButtonSelectedImageIDClicked: function (sender, args) {
        this._selectorSelectedImageID._uploaderView.add_onFileChanged(this._selectorSelectedImageIDUploaderViewFileChangedDelegate);
        this._SelectedImageIDDialog.dialog("open");
        jQuery("#designerLayoutRoot").hide();
        this._SelectedImageIDDialog.dialog().parent().css("min-width", "655px");
        dialogBase.resizeToContent();
        try {
            this._selectorSelectedImageID.get_uploaderView().get_altTextField().set_value("");
        }
        catch (ex) { }
        jQuery(this._selectorSelectedImageID.get_uploaderView().get_settingsPanel()).hide();
        return false;
    },

    _deselectButtonSelectedImageIDClicked: function (sender, args) {
        jQuery("#previewSelectedImageID").hide();
        jQuery("#previewSelectedImageID").attr("src", "");
        this.get_selectedSelectedImageID().innerHTML = "00000000-0000-0000-0000-000000000000";
        this.get_selectButtonSelectedImageID().innerHTML = "<span class=\"sfLinkBtnIn\">Select...</span>";
        jQuery(this.get_deselectButtonSelectedImageID()).hide()
        dialogBase.resizeToContent();
        return false;
    },



    /* --------------------------------- private methods --------------------------------- */
    /* SelectedImageID private methods */
    _selectorSelectedImageIDInsertHandler: function (selectedItem) {

        if (selectedItem) {
            this._SelectedImageIDId = selectedItem.Id;
            this.get_selectedSelectedImageID().innerHTML = this._SelectedImageIDId;
            jQuery(this.get_deselectButtonSelectedImageID()).show()
            this.get_selectButtonSelectedImageID().innerHTML = "<span class=\"sfLinkBtnIn\">Change</span>";
            jQuery("#previewSelectedImageID").show();
            jQuery("#previewSelectedImageID").attr("src", selectedItem.ThumbnailUrl);
        }
        this._SelectedImageIDDialog.dialog("close");
        jQuery("#designerLayoutRoot").show();
        dialogBase.resizeToContent();
    },

    _selectorSelectedImageIDCloseHandler: function () {
        if (this._SelectedImageIDDialog) {
            this._SelectedImageIDDialog.dialog("close");
        }
        jQuery("#designerLayoutRoot").show();
        dialogBase.resizeToContent();
    },

    _selectorSelectedImageIDUploaderViewFileChangedHandler: function () {
        dialogBase.resizeToContent();
    },

    /* SelectedPageID private methods */
    _showPageSelectorSelectedPageIDHandler: function (selectedItem) {
        var controlData = this._propertyEditor.get_control();
        var pagesSelector = this.get_pageSelectorSelectedPageID().get_pageSelector();
        if (controlData.SelectedPageID) {
            pagesSelector.set_selectedItems([{ Id: controlData.SelectedPageID }]);
        }
        this._SelectedPageIDDialog.dialog("open");
        jQuery("#designerLayoutRoot").hide();
        this._SelectedPageIDDialog.dialog().parent().css("min-width", "355px");
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

    /* Message properties */
    get_message: function () { return this._message; },
    set_message: function (value) { this._message = value; },
    get_buttonlabel: function () { return this._buttonlabel; },
    set_buttonlabel: function (value) { this._buttonlabel = value; },
    get_title: function () { return this._title; },
    set_title: function (value) { this._title = value; },
    get_style: function () { return this._style; },
    set_style: function (value) { this._style = value; },
    get_linkText: function () { return this._linkText; },
    set_linkText: function (value) { this._linkText = value; },
    get_imageSize: function () { return this._imageSize; },
    set_imageSize: function (value) { this._imageSize = value; },


    /* SelectedImageID properties */
    get_selectorSelectedImageID: function () {
        return this._selectorSelectedImageID;
    },
    set_selectorSelectedImageID: function (value) {
        this._selectorSelectedImageID = value;
    },
    get_selectButtonSelectedImageID: function () {
        return this._selectButtonSelectedImageID;
    },
    set_selectButtonSelectedImageID: function (value) {
        this._selectButtonSelectedImageID = value;
    },
    get_deselectButtonSelectedImageID: function () {
        return this._deselectButtonSelectedImageID;
    },
    set_deselectButtonSelectedImageID: function (value) {
        this._deselectButtonSelectedImageID = value;
    },
    get_selectedSelectedImageID: function () {
        if (this._selectedSelectedImageID == null) {
            this._selectedSelectedImageID = this.findElement("selectedSelectedImageID");
        }
        return this._selectedSelectedImageID;
    },

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
}

SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockWidget.Designer.SingleBlockWidgetDesigner.registerClass('SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockWidget.Designer.SingleBlockWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
