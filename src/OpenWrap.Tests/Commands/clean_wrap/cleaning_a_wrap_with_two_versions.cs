﻿using NUnit.Framework;
using OpenWrap.Commands;
using OpenWrap.Commands.contexts;
using OpenWrap.Commands.Wrap;
using OpenWrap.Repositories;
using OpenWrap.Testing;

namespace Tests.Commands.clean_wrap
{
    public class cleaning_a_wrap_with_two_versions : command_context<CleanWrapCommand>
    {
        static readonly string LionelVersion = "1.0.0.123";
        public cleaning_a_wrap_with_two_versions()
        {
            given_project_repository(new InMemoryRepository("Project repository"));
            given_project_package("lionel", "1.0.0.0");
            given_project_package("lionel", LionelVersion);
            given_dependency("depends: lionel");
            when_executing_command("lionel", "-Project");
        }

        [Test]
        public void only_the_latest_is_kept()
        {
            Environment.ProjectRepository
                    .PackagesByName["lionel"]
                    .ShouldHaveCountOf(1)
                    .ShouldHaveAll(v => v.Version.ToString().Equals(LionelVersion));
        }
        [Test]
        public void reported_no_errors()
        {
            Results.ShouldHaveNo(x => x.Type == CommandResultType.Error);
        }
    }
}