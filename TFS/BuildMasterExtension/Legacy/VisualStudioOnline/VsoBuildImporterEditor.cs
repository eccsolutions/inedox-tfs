﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using Inedo.BuildMaster;
using Inedo.BuildMaster.Extensibility.BuildImporters;
using Inedo.BuildMaster.Web.Controls.Extensions.BuildImporters;
using Inedo.BuildMasterExtensions.TFS.Clients.Rest;
using Inedo.Web.Controls;

namespace Inedo.BuildMasterExtensions.TFS.VisualStudioOnline
{
    internal sealed class VsoBuildImporterEditor : BuildImporterEditorBase<VsoBuildImporterTemplate>
    {
        private ValidatingTextBox txtBuildNumber;

        public override BuildImporterBase CreateFromForm()
        {
            var importer = new VsoBuildImporter
            {
                ArtifactName = Util.CoalesceStr(this.Template.ArtifactName, this.Template.TeamProject),
                TeamProject = this.Template.TeamProject,
                BuildDefinition = this.Template.BuildDefinition,
                TfsBuildNumber = (this.txtBuildNumber.Text != "last succeeded build" && this.txtBuildNumber.Text != "last completed build") ? this.txtBuildNumber.Text : null,
                IncludeUnsuccessful = this.txtBuildNumber.Text == "last completed build",
                ServerId = this.Template.ServerId,
                CreateBuildNumberVariable = this.Template.CreateBuildNumberVariable
            };

            if (AH.ParseInt(importer.TfsBuildNumber) == null)
            {
                var config = (TfsConfigurer)this.GetExtensionConfigurer();

                var api = new TfsRestApi(config);

                var tfsBuild = api.GetBuildsAsync(
                    project: importer.TeamProject,
                    buildNumber: importer.TfsBuildNumber,
                    resultFilter: importer.IncludeUnsuccessful ? "" : "succeeded",
                    statusFilter: "completed",
                    top: 1
                  ).Result()
                   .FirstOrDefault();

                if (tfsBuild == null)
                    throw new InvalidOperationException("There were no matching builds found in TFS.");

                var group = Regex.Match(tfsBuild.buildNumber, this.Template.BuildNumberPattern).Groups["num"];
                if (!group.Success || string.IsNullOrEmpty(group.Value))
                    throw new InvalidOperationException("A build number could not be extracted using " + this.Template.BuildNumberPattern + " from TFS build number " + tfsBuild.buildNumber);

                if (group.Value.Length > 10)
                    throw new InvalidOperationException("The extracted build number (" + group.Value + ") is longer than 10 characters.");

                importer.BuildNumber = group.Value;
            }

            return importer;
        }

        protected override void CreateChildControls()
        {
            this.txtBuildNumber = new ValidatingTextBox
            {
                AutoCompleteValues = new[] { "last completed build", "last succeeded build" },
                Text = this.Template.BuildNumberLocked ? (this.Template.IncludeUnsuccessful ? "last completed build" : "last succeeded build") : string.Empty,
                Enabled = !this.Template.BuildNumberLocked,
                DefaultText = "last succeeded build"
            };

            this.Controls.Add(
                new SlimFormField("Team project:", this.Template.TeamProject),
                new SlimFormField("Build definition:", this.Template.BuildDefinition),
                new SlimFormField("TFS build number:", this.txtBuildNumber)
            );
        }
    }
}
