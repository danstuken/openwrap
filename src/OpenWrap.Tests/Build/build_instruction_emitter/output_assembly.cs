﻿using NUnit.Framework;
using OpenWrap.Tests.Build.build_instruction_emitter_specs.contexts;

namespace OpenWrap.Build.build_instruction_emitter
{
    public class output_assembly : msbuild_emitter
    {
        public output_assembly()
        {
            given_export_name("bin-net35");
            given_output_assembly("sauron.dll");
            when_generating_instructions();
        }

        [Test]
        public void is_exported()
        {
            should_have_file("bin-net35", "sauron.dll");
        }
    }
}