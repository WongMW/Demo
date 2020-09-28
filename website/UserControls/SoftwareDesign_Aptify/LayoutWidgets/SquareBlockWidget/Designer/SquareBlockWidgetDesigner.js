Type.registerNamespace("SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SquareBlockWidget.Designer");

SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SquareBlockWidget.Designer.SquareBlockWidgetDesigner = function (element) {
    /* Initialize Message fields */
    this._message = null;
    this._title = null;
    this._subtitle = null;
    this._style = null;
    this._linkType = null;
    this._imageSize = null;

    /* Initialize SelectedPageID fields */
    this._pageSelectorSelectedPageID = null;
    this._selectorTagSelectedPageID = null;
    this._SelectedPageIDDialog = null;

    this._showPageSelectorSelectedPageIDDelegate = null;
    this._pageSelectedSelectedPageIDDelegate = null;

    /* Initialize the service url for the document thumbnails */
    this.documentServiceUrl = null;

    /* Initialize the service url for the image thumbnails */
    this.imageServiceUrl = null;
    
    /* Calls the base constructor */
    SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SquareBlockWidget.Designer.SquareBlockWidgetDesigner.initializeBase(this, [element]);
}

SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SquareBlockWidget.Designer.SquareBlockWidgetDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SquareBlockWidget.Designer.SquareBlockWidgetDesigner.callBaseMethod(this, 'initialize');

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

        jQuery(this.get_linkType()).find('input').change(function () {
            var linkType = $(this).val();
            if (linkType === 'Page') {
                $('.pageLinkCtrl').show();
                $('.externalLinkCtrl').hide();
                $('.documentLinkCtrl').hide();
            } else if (linkType === 'External') {
                $('.pageLinkCtrl').hide();
                $('.externalLinkCtrl').show();
                $('.documentLinkCtrl').hide();
            } else if (linkType === 'Document') {
                $('.pageLinkCtrl').hide();
                $('.externalLinkCtrl').hide();
                $('.documentLinkCtrl').show();
            }
        });
    },
    dispose: function () {
        /* this is the place to unbind/dispose the event handlers created in the initialize method */
        SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SquareBlockWidget.Designer.SquareBlockWidgetDesigner.callBaseMethod(this, 'dispose');

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
        jQuery(this.get_externalLink()).val(controlData.ExternalLink);
        jQuery(this.get_title()).val(controlData.Title);
        jQuery(this.get_subtitle()).val(controlData.Subtitle);
        jQuery(this.get_style()).val(controlData.Style);
        jQuery(this.get_linkType()).find('input[value="' + controlData.LinkType + '"]').click();
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

        /* RefreshUI SelectedDocumentID */
        this.get_selectedSelectedDocumentID().innerHTML = controlData.SelectedDocumentID;
        if (controlData.SelectedDocumentID && controlData.SelectedDocumentID != "00000000-0000-0000-0000-000000000000") {
            this.get_selectButtonSelectedDocumentID().innerHTML = "<span class=\"sfLinkBtnIn\">Change</span>";
            jQuery(this.get_deselectButtonSelectedDocumentID()).show()
            var url = this.documentServiceUrl + controlData.SelectedDocumentID + "/?published=true";
            jQuery.ajax({
                url: url,
                type: "GET",
                contentType: "application/json",
                dataType: "json",
                success: function (data) {
                    jQuery("#sqpreviewSelectedDocumentID").show();
                    jQuery("#sqpreviewSelectedDocumentID").text(data.Item.Title.Value);                    
                }
            });
        }
        else {
            jQuery(this.get_deselectButtonSelectedDocumentID()).hide()
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
        controlData.Title = jQuery(this.get_title()).val();
        controlData.Subtitle = jQuery(this.get_subtitle()).val();
        controlData.Style = jQuery(this.get_style()).val();
        controlData.ExternalLink = jQuery(this.get_externalLink()).val();
        controlData.ImageSize = jQuery(this.get_imageSize()).val();

        /* ApplyChanges SelectedImageID */
        controlData.SelectedImageID = this.get_selectedSelectedImageID().innerHTML;
        controlData.SelectedDocumentID = this.get_selectedSelectedDocumentID().innerHTML;
        controlData.LinkType = jQuery(this.get_linkType()).find('input:checked').val();
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
        jQuery("#sqpreviewSelectedDocumentID").hide();
        jQuery("#sqpreviewSelectedDocumentID").text("");
        this.get_selectedSelectedDocumentID().innerHTML = "00000000-0000-0000-0000-000000000000";
        this.get_selectButtonSelectedDocumentID().innerHTML = "<span class=\"sfLinkBtnIn\">Select...</span>";
        jQuery(this.get_deselectButtonSelectedDocumentID()).hide()
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

    /* SelectedDocumentID private methods */
    _selectorSelectedDocumentIDInsertHandler: function (selectedItem) {

        if (selectedItem) {
            this._SelectedDocumentIDId = selectedItem.Id;
            this.get_selectedSelectedDocumentID().innerHTML = this._SelectedDocumentIDId;
            jQuery(this.get_deselectButtonSelectedDocumentID()).show()
            this.get_selectButtonSelectedDocumentID().innerHTML = "<span class=\"sfLinkBtnIn\">Change</span>";
            jQuery("#sqpreviewSelectedDocumentID").show();
            jQuery("#sqpreviewSelectedDocumentID").text(selectedItem.Title);
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
    get_title: function () { return this._title; },
    set_title: function (value) { this._title = value; },
    get_subtitle: function () { return this._subtitle; },
    set_subtitle: function (value) { this._subtitle = value; },
    get_style: function () { return this._style; },
    set_style: function (value) { this._style = value; },
    get_externalLink: function () { return this._externalLink; },
    set_externalLink: function (value) { this._externalLink = value; },
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

    /* radio button properties */
    get_linkType: function () { return this._linkType; },
    set_linkType: function (value) { this._linkType = value; },

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

SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SquareBlockWidget.Designer.SquareBlockWidgetDesigner.registerClass('SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SquareBlockWidget.Designer.SquareBlockWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
