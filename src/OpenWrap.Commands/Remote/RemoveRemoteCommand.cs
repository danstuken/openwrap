﻿using System;
using System.Collections.Generic;
using OpenWrap.Collections;
using OpenWrap.Configuration;

namespace OpenWrap.Commands.Remote
{
    [Command(Noun="remote", Verb="remove")]
    public class RemoveRemoteCommand : AbstractCommand
    {
        [CommandInput(Position=0, IsRequired=true)]
        public string Name { get; set; }

        IConfigurationManager ConfigurationManager { get { return Services.ServiceLocator.GetService<IConfigurationManager>(); } }
        protected override IEnumerable<Func<IEnumerable<ICommandOutput>>> Validators()
        {
            yield return ValidateNameDoesntExist;
        }

        protected override IEnumerable<ICommandOutput> ExecuteCore()
        {
            var repositories = ConfigurationManager.LoadRemoteRepositories();
            
            repositories.Remove(Name);

            ConfigurationManager.SaveRemoteRepositories(repositories);
            yield return new GenericMessage(string.Format("Repository '{0}' removed.", Name));
        }


        IEnumerable<ICommandOutput> ValidateNameDoesntExist()
        {
            if (!ConfigurationManager.LoadRemoteRepositories().ContainsKey(Name))
                yield return new Error("Remote repository '{0}' not found.", Name);
        }
    }
}
