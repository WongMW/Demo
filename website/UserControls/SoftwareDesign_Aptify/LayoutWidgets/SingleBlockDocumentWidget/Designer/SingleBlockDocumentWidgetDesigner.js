Type.registerNamespace("SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockDocumentWidget.Designer");

SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockDocumentWidget.Designer.SingleBlockDocumentWidgetDesigner = function (element) {
    /* Initialize Message fields */
    this._message = null;
    this._buttonlabel = null;
    this._title = null;
    this._style = null;
    this._linkType = null;

    /* Initialize the service url for the image thumbnails */
    this.imageServiceUrl = null;

    /* Initialize the service url for the document thumbnails */
    this.documentServiceUrl = null;

    /* Calls the base constructor */
    SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockDocumentWidget.Designer.SingleBlockDocumentWidgetDesigner.initializeBase(this, [element]);
}

SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockDocumentWidget.Designer.SingleBlockDocumentWidgetDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockDocumentWidget.Designer.SingleBlockDocumentWidgetDesigner.callBaseMethod(this, 'initialize');

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

        /* Initialize SelectedDocumentID */
        this._selectButtonSelectedDocumentIDClickDelegate = Function.createDelegate(this, this._selectButtonSelectedDocumentIDClicked);
        if (this._selectButtonSelectedDocumentID) {
            $addHandler(this._selectButtonSelectedDocumentID, "click", this._selectButtonSelectedDocumentIDClickDelegate);
        }

        this._deselectButtonSelectedDocumentIDClickDelegate = Function.createDelegate(this, this._deselectButtonSelectedDocumentIDClicked);
        if (this._deselectButtonSelectedDocumentID) {
            $addHandler(this._deselectButtonSelectedDocumentID, "click", this._deselectButtonSelectedDocumentIDClickDelegate);
        }

        if (this._selectorSelectedDocumentID) {
            this._SelectedDocumentIDDialog = jQuery(this._selectorSelectedDocumentID.get_element()).dialog({
                autoOpen: false,
                modal: false,
                width: 655,
                height: "auto",
                closeOnEscape: true,
                resizable: false,
                draggable: false,
                zIndex: 5000,
                close: this._selectorSelectedDocumentIDCloseDelegate
            });
        }

        this._selectorSelectedDocumentIDInsertDelegate = Function.createDelegate(this, this._selectorSelectedDocumentIDInsertHandler);
        this._selectorSelectedDocumentID.set_customInsertDelegate(this._selectorSelectedDocumentIDInsertDelegate);
        $addHandler(this._selectorSelectedDocumentID._cancelLink, "click", this._selectorSelectedDocumentIDCloseHandler);
        this._selectorSelectedDocumentIDCloseDelegate = Function.createDelegate(this, this._selectorSelectedDocumentIDCloseHandler);
        this._selectorSelectedDocumentIDUploaderViewFileChangedDelegate = Function.createDelegate(this, this._selectorSelectedDocumentIDUploaderViewFileChangedHandler);
    },
    dispose: function () {
        /* this is the place to unbind/dispose the event handlers created in the initialize method */
        SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockDocumentWidget.Designer.SingleBlockDocumentWidgetDesigner.callBaseMethod(this, 'dispose');

        /* Dispose SelectedDocumentID */
        if (this._selectButtonSelectedDocumentID) {
            $removeHandler(this._selectButtonSelectedDocumentID, "click", this._selectButtonSelectedDocumentIDClickDelegate);
        }
        if (this._selectButtonSelectedDocumentIDClickDelegate) {
            delete this._selectButtonSelectedDocumentIDClickDelegate;
        }

        if (this._deselectButtonSelectedDocumentID) {
            $removeHandler(this._deselectButtonSelectedDocumentID, "click", this._deselectButtonSelectedDocumentIDClickDelegate);
        }
        if (this._deselectButtonSelectedDocumentIDClickDelegate) {
            delete this._deselectButtonSelectedDocumentIDClickDelegate;
        }

        $removeHandler(this._selectorSelectedDocumentID._cancelLink, "click", this._selectorSelectedDocumentIDCloseHandler);

        if (this._selectorSelectedDocumentIDCloseDelegate) {
            delete this._selectorSelectedDocumentIDCloseDelegate;
        }

        if (this._selectorSelectedDocumentIDUploaderViewFileChangedDelegate) {
            this._selectorSelectedDocumentID._uploaderView.remove_onFileChanged(this._selectorSelectedDocumentIDUploaderViewFileChangedDelegate);
            delete this._selectorSelectedDocumentIDUploaderViewFileChangedDelegate;
        }


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
            this._selectorSelectedImageID._uploaderView
                .remove_onFileChanged(this._selectorSelectedImageIDUploaderViewFileChangedDelegate);
            delete this._selectorSelectedImageIDUploaderViewFileChangedDelegate;
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

        /* RefreshUI SelectedDocumentID */
        this.get_selectedSelectedDocumentID().innerHTML = controlData.SelectedDocumentID;
        if (controlData.SelectedDocumentID && controlData.SelectedDocumentID != "00000000-0000-0000-0000-000000000000") {
            jQuery(this.get_selectedSelectedDocumentTitle()).show();
            this.get_selectButtonSelectedDocumentID().innerHTML = "<span class=\"sfLinkBtnIn\">Change</span>";
            jQuery(this.get_deselectButtonSelectedDocumentID()).show()
            var getSelectedSelectedDocumentTitle = this.get_selectedSelectedDocumentTitle();
            var url = this.documentServiceUrl + controlData.SelectedDocumentID + "/?published=true";
            jQuery.ajax({
                url: url,
                type: "GET",
                contentType: "application/json",
                dataType: "json",
                success: function (data) {
                    jQuery(getSelectedSelectedDocumentTitle).show();
                    jQuery(getSelectedSelectedDocumentTitle).text(data.Item.Title.Value);
                }
            });
        }
        else {
            jQuery(this.get_deselectButtonSelectedDocumentID()).hide()
            jQuery(this.get_selectedSelectedDocumentTitle()).show();
            jQuery(this.get_selectedSelectedDocumentTitle()).text("");
        }


        /* RefreshUI Message */
        jQuery(this.get_message()).val(controlData.Message);
        jQuery(this.get_buttonlabel()).val(controlData.ButtonLabel);
        jQuery(this.get_title()).val(controlData.Title);
        jQuery(this.get_style()).val(controlData.Style);
        jQuery(this.get_linkType()).find('input[value="' + controlData.LinkType + '"]').click();

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
     
    },

    /* Called when the "Save" button is clicked. Here you can transfer the settings from the designer to the control */
    applyChanges: function () {
        var controlData = this._propertyEditor.get_control();

        /* ApplyChanges Message */
        controlData.Message = jQuery(this.get_message()).val();
        controlData.ButtonLabel = jQuery(this.get_buttonlabel()).val();
        controlData.Title = jQuery(this.get_title()).val();
        controlData.Style = jQuery(this.get_style()).val();
        controlData.LinkType = jQuery(this.get_linkType()).find('input:checked').val();

        /* ApplyChanges SelectedDocumentID */
        controlData.SelectedDocumentID = this.get_selectedSelectedDocumentID().innerHTML;

        /* ApplyChanges SelectedImageID */
        controlData.SelectedImageID = this.get_selectedSelectedImageID().innerHTML;
    },

    /* --------------------------------- event handlers ---------------------------------- */

    /* SelectedDocumentID event handlers */
    _selectButtonSelectedDocumentIDClicked: function (sender, args) {
        this._selectorSelectedDocumentID._uploaderView.add_onFileChanged(this._selectorSelectedDocumentIDUploaderViewFileChangedDelegate);
        this._SelectedDocumentIDDialog.dialog("open");
        jQuery("#designerLayoutRoot").hide();
        this._SelectedDocumentIDDialog.dialog().parent().css("min-width", "655px");
        dialogBase.resizeToContent();
        try {
            this._selectorSelectedDocumentID.get_uploaderView().get_altTextField().set_value("");
        }
        catch (ex) { }
        jQuery(this._selectorSelectedDocumentID.get_uploaderView().get_settingsPanel()).hide();
        return false;
    },

    _deselectButtonSelectedDocumentIDClicked: function (sender, args) {
        jQuery(this.get_selectedSelectedDocumentTitle()).text("");
        jQuery(this.get_selectedSelectedDocumentTitle()).hide();
        this.get_selectedSelectedDocumentID().innerHTML = "";
        this.get_selectButtonSelectedDocumentID().innerHTML = "<span class=\"sfLinkBtnIn\">Select...</span>";
        jQuery(this.get_deselectButtonSelectedDocumentID()).hide()
        dialogBase.resizeToContent();
        return false;
    },

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

    /* SelectedDocumentID private methods */
    _selectorSelectedDocumentIDInsertHandler: function (selectedItem) {

        if (selectedItem) {
            this._SelectedDocumentIDId = selectedItem.Id;
            this.get_selectedSelectedDocumentID().innerHTML = this._SelectedDocumentIDId;
            jQuery(this.get_deselectButtonSelectedDocumentID()).show()
            this.get_selectButtonSelectedDocumentID().innerHTML = "<span class=\"sfLinkBtnIn\">Change</span>";
            jQuery(this.get_selectedSelectedDocumentTitle()).show();
            jQuery(this.get_selectedSelectedDocumentTitle()).text(selectedItem.Title);
        } else {
            jQuery(this.get_selectedSelectedDocumentTitle()).text("");
            jQuery(this.get_selectedSelectedDocumentTitle()).hide();
        }
        this._SelectedDocumentIDDialog.dialog("close");
        jQuery("#designerLayoutRoot").show();
        dialogBase.resizeToContent();
    },

    _selectorSelectedDocumentIDCloseHandler: function () {
        if (this._SelectedDocumentIDDialog) {
            this._SelectedDocumentIDDialog.dialog("close");
        }
        jQuery("#designerLayoutRoot").show();
        dialogBase.resizeToContent();
    },

    _selectorSelectedDocumentIDUploaderViewFileChangedHandler: function () {
        dialogBase.resizeToContent();
    },

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

    /* SelectedDocumentID properties */
    get_selectorSelectedDocumentID: function () {
        return this._selectorSelectedDocumentID;
    },
    set_selectorSelectedDocumentID: function (value) {
        this._selectorSelectedDocumentID = value;
    },
    get_selectButtonSelectedDocumentID: function () {
        return this._selectButtonSelectedDocumentID;
    },
    set_selectButtonSelectedDocumentID: function (value) {
        this._selectButtonSelectedDocumentID = value;
    },
    get_deselectButtonSelectedDocumentID: function () {
        return this._deselectButtonSelectedDocumentID;
    },
    set_deselectButtonSelectedDocumentID: function (value) {
        this._deselectButtonSelectedDocumentID = value;
    },
    get_selectedSelectedDocumentID: function () {
        if (this._selectedSelectedDocumentID == null) {
            this._selectedSelectedDocumentID = this.findElement("selectedSelectedDocumentID");
        }
        return this._selectedSelectedDocumentID;
    },
    get_selectedSelectedDocumentTitle: function () {
        if (this._selectedSelectedDocumentTitle == null) {
            this._selectedSelectedDocumentTitle = this.findElement("selectedSelectedDocumentTitle");
        }
        return this._selectedSelectedDocumentTitle;
    },


    /* radio button properties */
    get_linkType: function () { return this._linkType; },
    set_linkType: function (value) { this._linkType = value; }
}

SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockDocumentWidget.Designer.SingleBlockDocumentWidgetDesigner.registerClass('SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockDocumentWidget.Designer.SingleBlockDocumentWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
