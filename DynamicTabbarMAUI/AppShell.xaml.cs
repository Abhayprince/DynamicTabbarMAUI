using DynamicTabbarMAUI.Pages;

namespace DynamicTabbarMAUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // for the current users identity/role
        // You can get from
        // Preferences
        // or SQLite database
        // or maybe from some Api

        // This Database Fetch or Api Fetch Request should be blocking here
        // We shuld not use async here as our app's main Shell Tabbar depends on the response
        // so we will block the main thread here until we get te response back from db/api

        // Once you have user's identity
        // Then we can add dynamic tabbar items to the tabbar

        // For this particular Blog App
        // We have two type of users
        // Consumer
        // Publisher

        //var items = GetConsumerTabbarItems();
        //var items = GetPublisherTabbarItems();

        TabbarItem[] items = [];

        var currentUserRole = "Consumer"; // Get from Db/Api/Preferences

        if(currentUserRole == "Consumer")
        {
            items = GetConsumerTabbarItems();
        }
        else
        {
            items = GetPublisherTabbarItems();
        }

        //var items = GetConsumerTabbarItems();
        //var items = GetPublisherTabbarItems();

        var tabbarItems = items.Select(i => new ShellContent
        {
            Title = i.Name,
            Icon = i.Icon,
            Route = nameof(i.Type),
            ContentTemplate = new DataTemplate(i.Type)
        });
        tabbar.Items.Clear();
        foreach (var item in tabbarItems)
        {
            tabbar.Items.Add(item);
        }
    }

    record TabbarItem (string Name, string Icon, Type Type);

    private TabbarItem[] GetConsumerTabbarItems()
    {
        // 3 tabbr items - Home, Favourites, Profile

        //var home = new ShellContent
        //{
        //    Title = "Home",
        //    Icon = "home.png",
        //    Route = nameof(HomePage),
        //    ContentTemplate = new DataTemplate(typeof(HomePage)),
        //};

        TabbarItem[] items = [
            new TabbarItem("Home", "home.png", typeof(HomePage)),
            new TabbarItem("Favories", "favorites.png", typeof(FavoritesPage)),
            new TabbarItem("Profile", "user_profile.png", typeof(ProfilePage)),
            ];

        return items;
        
    }
    private TabbarItem[] GetPublisherTabbarItems()
    {
        // 4 tabbar items - Home, create Post, Manage Posts, Profile

        TabbarItem[] items = [
            new TabbarItem("Home", "home.png", typeof(PublisherHomePage)),
            new TabbarItem("Add Post", "create_post.png", typeof(CreatePostPage)),
            new TabbarItem("Manage Posts", "manage_posts.png", typeof(managePostsPage)),
            new TabbarItem("Profile", "user_profile.png", typeof(ProfilePage)),
            ];

        return items;        
    }
}
