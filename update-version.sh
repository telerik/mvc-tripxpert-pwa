echo "NPM version: npm show @progress/kendo-ui version"
#npm show @progress/kendo-ui version
#curl  https://api.github.com/repos/telerik/kendo-ui-core/releases
#echo $("curl -s https://api.github.com/repos/telerik/kendo-ui-core/releases")
#echo "Release version: curl -s https://api.github.com/repos/telerik/kendo-ui-core/releases | grep tag_name | head -n 1 |  cut -d '\"' -f 4"
#curl -s https://api.github.com/repos/telerik/kendo-ui-core/releases | grep tag_name | head -n 1 |  cut -d '"' -f 4



#CURRENT_VERSION="2021.1.119"
CURRENT_VERSION=$(grep -hnr "kendo.cdn" TripXpert/Views/Shared/_Layout.cshtml | head -1 |cut -d '/' -f 4)
echo $CURRENT_VERSION
LAST_RELEASE=$(curl -s https://api.github.com/repos/telerik/kendo-ui-core/releases | grep tag_name | head -n 1 |  cut -d '"' -f 4)
echo $LAST_RELEASE

#sed -i "s/$CURRENT_VERSION/$LAST_RELEASE/g" TripXpert/Views/Shared/_Layout.cshtml

sed -i "s/kendo.cdn.telerik.com\/[^\/]*/kendo.cdn.telerik.com\/$LAST_RELEASE/g" TripXpert/Views/Shared/_Layout.cshtml

