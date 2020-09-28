# Setup DEV ENV
- Create IIS website with 32 bits apps set to false (on the application pool advanced settings)
- Open the [projectfolder]\website\web.config search for "Aptify.Framework.ObjectRepository.AptifyObjectRepository.DefaultObjectCachePath" and change the parameter value to your local website/bin folder (FULL PHYSICAL PATH)
- Open the [projectfolder]\website\web.config search for "HTTP/S to HTTPS Redirect" and comment all this 'rule' block of code
- Open the [projectfolder]\website\web.config search for '<add assembly="log4net, Version=1.2.10.0, Culture=neutral, PublicKeyToken=1b44e1d426115821" />' and comment this line
- Open the [projectfolder]\website\web.config search forcustomErrors and switch the parameter 'mode' to Off
- Open the [projectfolder]\website\App_Data\Sitefinity\Configuration\SystemConfig.config search for loadBalancingConfig and comment the block inside parameters node
- Open the [projectfolder]\website\App_Data\Sitefinity\Configuration\SystemConfig.config search for replaceSiteUrl and change the parameter siteUrl to your target domain, default is www.charteredaccountants.ie
- If you're out of the SD office ensure VPN is on (DB in the local network)
- Default Sitefinity login is admin/password
- Allow IP security
    all you have to do is edit the applicationhost.config file for IIS Express found here: [projectfolder]\.vs\CAI\config\applicationhost.config
    Open it up and look for the ipSecurity and dynamicIpSecurity sections they are inside the system.webServer section and change overrideModeDefault from "Deny" to "Allow".


AFTER ALL CHANGES ABOVE, PLEASE DON'T INCLUDE THE WEB.CONFIG IN YOUR COMMITS, IF YOU CHANGE SOMETHING IN THIS FILES, FIRST YOU NEED TO UNDO ALL THESE CONFIGURATION CHANGES TO WORK IN YOUR LOCAL ENVIRONMENT.

###### In case of doubt we hope that the following document can help you: [GitHub Process](https://charteredaccountantsireland.sharepoint.com/:w:/r/sites/Sogeti/_layouts/15/Doc.aspx?sourcedoc=%7BDE02FB36-589B-43D5-946D-45664CBB5631%7D&file=GIT%20Process.docx&action=default&mobileredirect=true&cid=8c5c576a-1439-4962-ad7e-6b679a4b2741)
