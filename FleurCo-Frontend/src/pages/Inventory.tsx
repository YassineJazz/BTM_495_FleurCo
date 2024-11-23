import { createResource, createSignal, For } from "solid-js"
import Layout from "../components/Layout"
import { getInventory, InventoryProduct } from "../utils/api"
import { useNavigate } from "@solidjs/router";

export const Inventory = () => {
    const [items] = createResource<InventoryProduct[]>(getInventory);
    const [search, setSearch] = createSignal("");
    const navigate = useNavigate();


    const fileredItems = () => {
        if (search() === "") {
            return items();
        }
        return items()?.filter((item) =>
            item.productName.toLowerCase().includes(search().toLowerCase())
            || item.productCost.toString().includes(search().toLowerCase())
            || item.productPrice.toString().includes(search().toLowerCase())
            || item.productCategory.toLowerCase().includes(search().toLowerCase())
            || item.productId.toLowerCase().includes(search().toLowerCase())
            || item.inventoryId.toLowerCase().includes(search().toLowerCase())
            || item.quantity.toString().includes(search().toLowerCase())
        );
    }

    const handleKeyDown = (e: KeyboardEvent, inventoryId: string) => {
        if (e.key === "Enter" || e.key === " ") {
            e.preventDefault();
            navigate(`/inventory/${inventoryId}`);
        }
    };

    return (
        <Layout>
            <div class="flex flex-col h-full w-full gap-4 p-4">
                <div class="px-2 flex flex-col gap-2">
                    <h1 class="text-3xl font-bold"> FleurCo </h1>
                    <div class="flex flex-row justify-between items-center">
                        <h2 class="text-3xl font-semi-bold"> Inventory </h2>
                        <div class="flex flex-row justify-center items-center gap-2">
                            <svg xmlns="http://www.w3.org/2000/svg" width="36" height="36" viewBox="0 0 24 24"><path d="M19 7.001c0 3.865-3.134 7-7 7s-7-3.135-7-7c0-3.867 3.134-7.001 7-7.001s7 3.134 7 7.001zm-1.598 7.18c-1.506 1.137-3.374 1.82-5.402 1.82-2.03 0-3.899-.685-5.407-1.822-4.072 1.793-6.593 7.376-6.593 9.821h24c0-2.423-2.6-8.006-6.598-9.819z" /></svg>                            <div class="flex flex-col justify-start items-start">
                                <label class="text-3x1 font-bold text-base"> Bryan Doe</label>
                                <label class="text-3x1 font-semi-bold text-sm">Warehouse Manager</label>
                            </div>
                        </div>

                    </div>
                    <label class="input input-bordered flex items-center gap-2">
                        <input type="text" class="grow" placeholder="Search by ID, Name, Price, Cost, Category, Quantity..." value={search()} onInput={(e) => setSearch(e.currentTarget.value)} />
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 16 16"
                            fill="currentColor"
                            class="h-4 w-4 opacity-70">
                            <path
                                fill-rule="evenodd"
                                d="M9.965 11.026a5 5 0 1 1 1.06-1.06l2.755 2.754a.75.75 0 1 1-1.06 1.06l-2.755-2.754ZM10.5 7a3.5 3.5 0 1 1-7 0 3.5 3.5 0 0 1 7 0Z"
                                clip-rule="evenodd" />
                        </svg>
                    </label>
                </div>

                <div class="overflow-x-auto p-2 h-full">
                    <table class="table">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Category</th>
                                <th>Price ($)</th>
                                <th>Cost ($) </th>
                                <th>Quantity</th>
                            </tr>
                        </thead>
                        <tbody>
                            <For each={fileredItems()}>
                                {(item) => (
                                    <tr class="hover rounded-md focus:outline-none focus:ring focus:ring-base-content/20 focus:ring-offset-base-200 focus:ring-offset-2"
                                        role="button"
                                        tabIndex="0"
                                        onKeyDown={(e) => handleKeyDown(e, item.inventoryId)}
                                        onClick={() => navigate(`/inventory/${item.inventoryId}`)}
                                    >
                                        <td>{item.productName}</td>
                                        <td>{item.productCategory}</td>
                                        <td>{item.productPrice}</td>
                                        <td>{item.productCost}</td>
                                        <td>{item.quantity}</td>
                                    </tr>
                                )}
                            </For>
                        </tbody>
                    </table>
                </div>
            </div>
        </Layout >
    )
}
export default Inventory
