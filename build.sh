cd src/Frontend 
echo "Building frontend..."
dotnet restore && dotnet fable yarn-run build
[ $? != '0' ] && echo "Frontend build failed!" && exit 1
cd $OLDPWD 
cd src/Backend
echo "Building backend..."
./run.sh
cd $OLDPWD
