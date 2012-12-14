require 'rake'
require 'albacore'

task :default => :build

#
# Build Tasks

desc 'Builds the project to the Build\bin folder (only if changes have been made)'
msbuild :build do |msb|
    msb.targets :Build
    msb.properties = { :Configuration => 'Scripted' }
    msb.solution = 'Echo.sln'
end

#
# Testing Tasks

desc 'Performs all of the tests'
task :test => [:build, :acceptance_test, :unit_test]

desc 'Acceptance tests the code'
nunit :acceptance_test do |nunit|
    nunit.command = 'Packages\NUnit.Runners.2.6.2\tools\nunit-console-x86.exe'
    nunit.assemblies 'Build\bin\Testing\Acceptance\Acceptance.dll'
end

desc 'Unit tests the code and records the results'
mspec :unit_test do |mspec|
    mspec.command = 'Packages\Machine.Specifications.0.5.10\tools\mspec-clr4.exe'
    mspec.assemblies 'Build\bin\Testing\Unit\Unit.dll'
end

#
# Packaging Tasks

desc 'Packages the binaries to the Build\package folder'
task :package => :build do

    # Clear any existing packaged files
    folder = 'Build\package'
    sh "rmdir /q /s #{folder}" if Dir.exists? folder
    
    # Package the DLLs
    sh "xcopy /y /s /exclude:.pakignore Build\\bin\\Echo\\* #{folder}\\"
    
end