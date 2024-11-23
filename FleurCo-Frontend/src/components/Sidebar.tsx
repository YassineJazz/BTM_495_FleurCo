import SidebarItem from "./SidebarItem";

export const Sidebar = () => {
    return (
        <div class="h-full flex flex-col gap">
            <SidebarItem href="/" text="Home" />
            <SidebarItem href="/inventory" text="Inventory" />
            <SidebarItem href="/products" text="Product Line" />
            <SidebarItem href="/orders" text="Orders" />
        </div>
    )
}
export default Sidebar;
