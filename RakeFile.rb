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