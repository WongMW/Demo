1. Install https://www.iis.net/downloads/microsoft/application-request-routing
2. Open IIS (if opened, close and re-open)
3. Enable Reverse Proxy Settings - https://docs.microsoft.com/en-us/iis/extensions/configuring-application-request-routing-arr/creating-a-forward-proxy-using-application-request-routing
4. Create folder for charteredcareers and place web.config into the folder
5. Open Web.Config and change all dev.charteredaccountants.ie to www.charteredaccountants.ie and also change all charteredcareers.lightboxdigital.ie to charteredcareers.ie
6. Create Sub Application in IIS under charetered accountants website named study (please use separate application pool for it) and point to the folder created in step 4