// See https://aka.ms/new-console-template for more information
using electronicStore;
StoreLogicFactory storeLogicFactory = new StoreLogicFactory();

IStoreLogic storeLogic = storeLogicFactory.generator().Create();
storeLogic.createBaseWares();
storeLogic.InitializeWelcome();

