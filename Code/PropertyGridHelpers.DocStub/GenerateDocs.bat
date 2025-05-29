@Echo off

pushd bin\Debug\net472

PropertyGridHelpers.DocStub.exe ^
  PropertyGridHelpers.dll ^
  ../../../../../docs-md/net472 ^
  --source https://github.com/dparvin/PropertyGridHelpers/tree/master/Code/PropertyGridHelpers ^
  --visibility public ^
  --namespace-pages

popd
