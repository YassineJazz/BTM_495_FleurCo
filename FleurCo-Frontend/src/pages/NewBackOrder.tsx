import { createEffect, createMemo, createResource, createSignal, For } from "solid-js"
import Layout from "../components/Layout"
import Modal from "../components/Modal";
import { BackOrderRequest, createBackorder, getInventory, InventoryProduct } from "../utils/api"
import { useNavigate } from "@solidjs/router";
import { createStore } from "solid-js/store";
import { render, Portal, MountableElement } from "solid-js/web";
import Toast from "../components/Toast";

interface SelectedItem {
    quantity: number,
    selected: boolean,
    inventoryId: string
}
export const NewBackOrder = () => {
    const [items] = createResource<InventoryProduct[]>(getInventory);
    const [selected, setSelected] = createStore<SelectedItem[]>([]);
    const [search, setSearch] = createSignal("");
    const navigate = useNavigate();



    createEffect(() => {
        if (items()) {
            const newItems: SelectedItem[] = items()?.map(i => ({
                selected: false,
                inventoryId: i.inventoryId,
                quantity: 0

            }))!;
            setSelected([...newItems]);
        }
    });

    const onCheckboxChange = (e: Event, inventoryId: string) => {
        const target = e.target as HTMLInputElement;
        setSelected(item => item.inventoryId === inventoryId, "selected", target.checked);
    }

    const onQuantityChange = (e: Event, inventoryId: string) => {
        const target = e.currentTarget as HTMLInputElement;
        var value: string = target.value;
        if (value === "") {
            value = "0";
        }
        setSelected(item => item.inventoryId === inventoryId, "quantity", parseInt(value));
    }

    const onSubmit = async (e: Event) => {
        e.preventDefault();

        const itemsToSend: BackOrderRequest[] = selected.filter(i => i.selected).map(i => ({
            quantity: i.quantity,
            inventoryId: i.inventoryId
        }));
        await createBackorder(itemsToSend);
        const portalId = document.getElementById("portal");
        render(() => (
            <Portal>
                <Toast text={`Backorder successfully created`} duration={2500} />
            </Portal>
        ), portalId as MountableElement)
        navigate("/orders");
    }


    const disabled = createMemo(() => selected.every(i => !i.selected) || selected.filter(i => i.selected).some(i => i.quantity === 0));


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

    const checkAll = (e: Event) => {
        const target = e.currentTarget as HTMLInputElement;
        setSelected(items => items.map(i => ({
            ...i,
            selected: target.checked
        })));
    }

    const goBack = async () => {
        navigate("/orders");
    }

    return (
        <Layout>
            <div class="flex flex-col w-full gap-4 p-4">
                <div class="flex flex-col gap-2">
                    <h1 class="text-3xl font-bold"> FleurCo </h1>
                    <div class="flex flex-row justify-between items-center">
                        <h2 class="text-3xl font-semi-bold"> Product Line </h2>
                        <div class="flex flex-row justify-center items-center gap-2">
                            <svg xmlns="http://www.w3.org/2000/svg" width="36" height="36" viewBox="0 0 24 24"><path d="M19 7.001c0 3.865-3.134 7-7 7s-7-3.135-7-7c0-3.867 3.134-7.001 7-7.001s7 3.134 7 7.001zm-1.598 7.18c-1.506 1.137-3.374 1.82-5.402 1.82-2.03 0-3.899-.685-5.407-1.822-4.072 1.793-6.593 7.376-6.593 9.821h24c0-2.423-2.6-8.006-6.598-9.819z" /></svg>                            <div class="flex flex-col justify-start items-start">
                                <label class="text-3x1 font-bold text-base"> Bryan Doe</label>
                                <label class="text-3x1 font-semi-bold text-sm">Warehouse Manager</label>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="flex flex-col w-full gap-4">
                    <div class="flex flex-col gap-4">
                        <div class="flex flex-row justify-between items-center gap-8">
                            <div class="flex flex-col gap-4 items-start">
                                <Modal
                                    btnClass="btn btn-sm btn-neutral"
                                    btnText="Back"
                                    confirmText={`Are you sure you want to go back? Any changes will not be saved`}
                                    onSuccess={goBack}
                                />
                                <h1 class="text-2xl font-bold"> New Backorder </h1>
                            </div>
                            <p class="text-sm text-gray-500"> {selected.filter(i => i.selected).length} selected </p>
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

                    <form class="overflow-x-auto p-2" onSubmit={onSubmit}>
                        <table class="table">
                            <thead>
                                <tr>
                                    <th><input type="checkbox" class="checkbox checkbox-primary" onChange={(e) => checkAll(e)} checked={selected.every(i => i.selected)} />
                                    </th>
                                    <th>Name</th>
                                    <th>Price</th>
                                    <th>Cost</th>
                                    <th>Category</th>
                                    <th>Quantity</th>
                                    <th>Quantity to Order</th>
                                </tr>
                            </thead>
                            <tbody>
                                <For each={fileredItems()}>
                                    {(item) => (
                                        <tr class="hover rounded-md focus:outline-none focus:ring focus:ring-base-content/20 focus:ring-offset-base-200 focus:ring-offset-2"
                                        >
                                            <td>
                                                <input type="checkbox" class="checkbox checkbox-primary" onChange={(e) => onCheckboxChange(e, item.inventoryId)} checked={selected.find(i => i.inventoryId === item.inventoryId)?.selected} />
                                            </td>
                                            <td>{item.productName}</td>
                                            <td>{item.productPrice}</td>
                                            <td>{item.productCost}</td>
                                            <td>{item.productCategory}</td>
                                            <td>{item.quantity}</td>
                                            <td>
                                                <input
                                                    type="number" class="input input-bordered"
                                                    value={selected.find(i => i.inventoryId === item.inventoryId)?.quantity}
                                                    onInput={(e) => onQuantityChange(e, item.inventoryId)}
                                                />
                                            </td>
                                        </tr>
                                    )}
                                </For>
                            </tbody>
                        </table>
                        <button type="submit" class="btn btn-primary w-full" disabled={disabled()}> Create </button>
                    </form>
                </div>
            </div>
        </Layout>
    )
}
export default NewBackOrder
