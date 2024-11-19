import SidebarItem from "./SidebarItem";

export const Sidebar = () => {
    return (
        <div class="h-full flex flex-col gap">
            <SidebarItem href="/inventory" text="Inventory" />
            <SidebarItem href="/products" text="Product Line" />
            <SidebarItem href="/orders" text="Orders" />
            <SidebarItem href="/forecast" text="Forecast" />
        </div>
    )
}
export default Sidebar;
