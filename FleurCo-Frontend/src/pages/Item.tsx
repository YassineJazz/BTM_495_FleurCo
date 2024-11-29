import { useNavigate, useParams } from "@solidjs/router";
import Layout from "../components/Layout";
import Modal from "../components/Modal";
import { createEffect, createMemo, createResource, createSignal } from "solid-js";
import { updateQuantity, InventoryProduct, getItem } from "../utils/api";
import { render, Portal, MountableElement } from "solid-js/web";
import Toast from "../components/Toast";

const Item = () => {
    const params = useParams();
    const navigate = useNavigate();

    const [item] = createResource<InventoryProduct>(() => getItem(params.inventoryId));
    const [quantity, setQuantity] = createSignal("");


    const goBack = async () => {
        navigate("/inventory");
    }

    createEffect(() => {
        if (item()) {
            setQuantity(item()?.quantity.toString()!);

        }
    });

    const onSubmit = async (e: Event) => {
        e.preventDefault();

        await updateQuantity(params.inventoryId, parseInt(quantity()));
        const portalId = document.getElementById("portal");
        render(() => (
            <Portal>
                <Toast text={`${item()?.productName} successfully updated`} duration={2500} />
            </Portal>
        ), portalId as MountableElement);
        navigate("/inventory");
    }

    const disabled = createMemo(() => (quantity() === item()?.quantity.toString())
        || (quantity().toString() === ""));
    return (
        <Layout>
            <div class="flex flex-col w-full justify-between items-center p-4">
                <div class="flex flex-col h-full w-full gap-4">
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
                    <div class="flex flex-col gap-4 items-start">
                        <Modal
                            btnClass="btn btn-sm btn-neutral"
                            btnText="Back"
                            confirmText={`Are you sure you want to go back? Any changes will not be saved`}
                            onSuccess={goBack}
                        />
                        <p class="text-3xl font-semibold">Item info: {item()?.productName}</p>
                    </div>
                </div>
                <form class="flex flex-col gap-4 w-full h-full" onSubmit={onSubmit}>
                    <div class="flex flex-col gap">
                        <label for="name" class="label text-sm">Name: {item()?.productName}</label>
                    </div> <div class="flex flex-col gap">
                        <label for="price" class="label text-sm">Price ($): {item()?.productPrice.toFixed(2)}</label>
                    </div> <div class="flex flex-col gap">
                        <label for="cost" class="label text-sm">Cost ($): {item()?.productCost.toFixed(2)}</label>
                    </div>
                    <div class="flex flex-col gap">
                        <label for="category" class="label text-sm">Category: {item()?.productCategory}</label>
                    </div>
                    <div class="flex flex-col gap">
                        <label for="quantity" class="label text-sm">Quantity</label>
                        <input type="number" id="quantity" class="input input-bordered" value={quantity()} onInput={(e) => setQuantity(e.currentTarget.value)} />
                    </div>
                    <button class="btn btn-primary" type="submit" disabled={disabled()}>Save</button>
                </form>
            </div>
        </Layout>
    )
}
export default Item;
