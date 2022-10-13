using System;
using UnityEngine;
using UnityEngine.Purchasing;


public class IAPMgr : Singleton<IAPMgr>, IStoreListener
{
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    public string all_levels = "all_levels";
    public string kit_1 = "kit_1";
    public string kit_2 = "kit_2";
    public string kit_3 = "kit_3";
    public string kit_22 = "kit_22";
    public string kit_33 = "kit_33";
    public string kit_333 = "kit_333";


    void Start()
    {
        //Debug.Log(all_levels);
        //Debug.Log(kit_1);
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add a product to sell / restore by way of its identifier, associating the general identifier
        // with its store-specific identifiers.
        //builder.AddProduct(units_34, ProductType.Consumable);
        //builder.AddProduct(units_90, ProductType.Consumable);
        //builder.AddProduct(units_190, ProductType.Consumable);
        //builder.AddProduct(units_400, ProductType.Consumable);

        // Continue adding the non-consumable product.
        builder.AddProduct(all_levels, ProductType.NonConsumable);
        builder.AddProduct(kit_1, ProductType.NonConsumable);
        builder.AddProduct(kit_2, ProductType.NonConsumable);
        builder.AddProduct(kit_22, ProductType.NonConsumable);
        builder.AddProduct(kit_3, ProductType.NonConsumable);
        builder.AddProduct(kit_33, ProductType.NonConsumable);
        builder.AddProduct(kit_333, ProductType.NonConsumable);

        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }


    public bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    public void BuyAllLevels()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(all_levels);
    }
    public void BuyKit1()
    {
        BuyProductID(kit_1);
    }
    public void BuyKit2()
    {
        BuyProductID(kit_2);
    }
    public void BuyKit22()
    {
        BuyProductID(kit_22);
    }

    public void BuyKit3()
    {
        BuyProductID(kit_3);
    }
    public void BuyKit33()
    {
        BuyProductID(kit_33);
    }
    public void BuyKit333()
    {
        BuyProductID(kit_333);
    }


    public string getProductPriceFromStore(string id)
    {
        if (m_StoreController != null && m_StoreController.products != null) return m_StoreController.products.WithID(id).metadata.localizedPriceString;
        else return "";
    }

    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) =>
            {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // A consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, all_levels, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            MenuMngr.Instance.SetDefaultGraphics();
        }
        // A consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, kit_1, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            MenuMngr.Instance.SetMaximumGraphics(1);
        }
        // A consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, kit_2, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            MenuMngr.Instance.SetMaximumGraphics(2);
        }
        
        // A consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, kit_3, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            MenuMngr.Instance.SetMaximumGraphics(3);

        }
        // A consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, kit_22, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            MenuMngr.Instance.SetMaximumGraphics(2);

        }
        // A consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, kit_33, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            MenuMngr.Instance.SetMaximumGraphics(3);
        }
        // A consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, kit_333, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            MenuMngr.Instance.SetMaximumGraphics(3);
        }
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
