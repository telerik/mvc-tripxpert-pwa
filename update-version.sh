echo "NPM version: npm show @progress/kendo-ui version"
npm show @progress/kendo-ui version
#curl  https://api.github.com/repos/telerik/kendo-ui-core/releases
#echo $("curl -s https://api.github.com/repos/telerik/kendo-ui-core/releases")
echo "Release version: curl -s https://api.github.com/repos/telerik/kendo-ui-core/releases | grep tag_name | head -n 1 |  cut -d '\"' -f 4"
#curl -s https://api.github.com/repos/telerik/kendo-ui-core/releases | grep tag_name | head -n 1 |  cut -d '"' -f 4





LAST_RELEASE=$(curl -s https://api.github.com/repos/telerik/kendo-ui-core/releases | grep tag_name | head -n 1 |  cut -d '"' -f 4)
echo $LAST_RELEASE

