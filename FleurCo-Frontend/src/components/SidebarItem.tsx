import { useLocation } from "@solidjs/router";
import { Component, createMemo } from "solid-js";

interface SidebarItemProps {
    text: string;
    href: string;
}
export const SidebarItem: Component<SidebarItemProps> = (props) => {
    const location = useLocation();
    const active = createMemo(() => location.pathname === props.href);

    const classes = () => {
        const base = "p-2 rounded-xl font-semibold transition-colors duration-200 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-base-100 focus:ring-base-content/20";
        if (active()) {
            return "bg-base-100 " + base;
        }
        return "hover:bg-base-200/50 " + base;
    }
    return (
        <a class={classes()} href={props.href}>{props.text}</a>
    )
}
export default SidebarItem;
