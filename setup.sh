export GROUP_NAME=signalr-group
export LOCATION=WestEurope
export SIGNALR_NAME=signalr

ng new Azure.SignalR.Client

dotnet new mvc

az group create -g $GROUP_NAME -l $LOCATION
az signalr create -n $SIGNALR_NAME -g $GROUP_NAME -l $LOCATION --unit-count 1 --sku Basic
CONNECTION_STRING=`az signalr key list -n $SIGNALR_NAME -g $GROUP_NAME -o tsv --query "primaryConnectionString"`