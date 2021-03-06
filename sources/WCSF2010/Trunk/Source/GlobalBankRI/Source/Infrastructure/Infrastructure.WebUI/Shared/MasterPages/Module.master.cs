//===============================================================================
// Microsoft patterns & practices
// Web Client Software Factory 2010
//===============================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================
//===============================================================================
// Microsoft patterns & practices
// Web Client Software Factory
//-------------------------------------------------------------------------------
// Copyright (C) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//-------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================


using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class ModuleMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (!IsPostBack)
        {
            SiteMapNode moduleNode = GetModuleNode();
            if (moduleNode != null)
            {
                moduleNameLabel.Text = moduleNode.Title;
            }
        }
    }

    protected void ActionsMenu_MenuItemDataBound(object sender, MenuEventArgs e)
    {
        SiteMapNode siteMapNode = (SiteMapNode)e.Item.DataItem;
        SiteMapNode moduleNode = GetModuleNode();
        if (moduleNode != null && IsNodeActive(siteMapNode))
        {
            e.Item.ImageUrl = String.Format(ConfigurationManager.AppSettings["ModuleImagePathFormatString_Action"], GetModuleNode()["imageName"]);
        }
    }

    protected void ModulesMenu_MenuItemDataBound(object sender, MenuEventArgs e)
    {
        SiteMapNode siteMapNode = (SiteMapNode)e.Item.DataItem;

        string pathStringFormat = IsNodeActive(siteMapNode) ? ConfigurationManager.AppSettings["ModuleImagePathFormatString_Active"] : ConfigurationManager.AppSettings["ModuleImagePathFormatString_Inactive"];
        e.Item.ImageUrl = String.Format(pathStringFormat, siteMapNode["imageName"]);
    }

    private SiteMapNode GetModuleNode()
    {
        SiteMapNode currentNode = SiteMap.CurrentNode;
        while ((currentNode != null) && (currentNode.ParentNode != SiteMap.RootNode))
        {
            currentNode = currentNode.ParentNode;
        }
        return currentNode;
    }
    
    private bool IsNodeActive(SiteMapNode siteMapNode)
    {
        if (SiteMap.CurrentNode != null)
        {
            return (SiteMap.CurrentNode.Equals(siteMapNode) || SiteMap.CurrentNode.IsDescendantOf(siteMapNode));
        }
        return false;
    }
}
