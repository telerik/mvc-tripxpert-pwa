echo "NPM version: npm show @progress/kendo-ui version"
LAST_RELEASE=$(curl -s https://api.github.com/repos/telerik/kendo-ui-core/releases | grep tag_name | head -n 1 |  cut -d '"' -f 4)
echo $LAST_RELEASE
files=(TripXpert/TripXpert.csproj TripXpert/Views/Contact/Contact.cshtml TripXpert/Views/Destinations/DestinationDetails.cshtml TripXpert/Views/Destinations/Destinations.cshtml TripXpert/Views/Home/Index.cshtml TripXpert/Views/Shared/_Layout.cshtml TripXpert/packages.config TripXpert/service-worker.js)
for file in ${files[@]}
do
CURRENT_VERSION=$(grep -hnr "kendo.cdn" $file | head -1 |cut -d '/' -f 4)
echo "$file $CURRENT_VERSION"
sed -i "s/kendo.cdn.telerik.com\/[^\/]*/kendo.cdn.telerik.com\/$LAST_RELEASE/g" $file
done