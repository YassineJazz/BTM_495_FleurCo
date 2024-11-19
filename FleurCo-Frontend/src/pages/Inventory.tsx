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
            <div class="flex flex-col h-full w-full gap-4">
                <div class="px-2 flex flex-col gap-4">
                    <div class="flex flex-row justify-between items-center">
                        <h1 class="text-3xl font-bold"> Inventory </h1>
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
                                <th>Price</th>
                                <th>Cost</th>
                                <th>Category</th>
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
                                        <td>{item.productPrice}</td>
                                        <td>{item.productCost}</td>
                                        <td>{item.productCategory}</td>
                                        <td>{item.quantity}</td>
                                    </tr>
                                )}
                            </For>
                        </tbody>
                    </table>
                </div>
            </div>
        </Layout>
    )
}
export default Inventory
