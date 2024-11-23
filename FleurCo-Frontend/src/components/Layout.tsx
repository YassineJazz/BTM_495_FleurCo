import { Component, ParentProps } from "solid-js";
import Sidebar from "./Sidebar";

export const Layout: Component<ParentProps> = (props) => {
    return (
        <div class="w-full h-full flex">
            <div class="p-4 bg-base-300 w-[250px]">
                <Sidebar />
            </div>
            <div class="w-full">
                {props.children}
            </div>
        </div>

    )
}
export default Layout;
