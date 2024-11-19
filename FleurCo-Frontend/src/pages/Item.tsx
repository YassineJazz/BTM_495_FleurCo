import { useNavigate, useParams } from "@solidjs/router";
import Layout from "../components/Layout";
import { createEffect, createMemo, createResource, createSignal } from "solid-js";
import { updateQuantity, InventoryProduct, getItem } from "../utils/api";

const Item = () => {
    const params = useParams();
    const navigate = useNavigate();

    const [item] = createResource<InventoryProduct>(() => getItem(params.inventoryId));
    const [quantity, setQuantity] = createSignal("");


    const goBack = () => {
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
        navigate("/inventory");
    }

    const disabled = createMemo(() => (quantity() === item()?.quantity.toString())
        || (quantity().toString() === ""));
    return (
        <Layout>
            <div class="flex w-full justify-between items-center">
                <div class="flex gap-4">
                    <button class="btn btn-sm btn-neutral" onClick={goBack}>Back</button>
                    <p class="text-3xl font-semibold">Item info: {item()?.productName}</p>
                </div>
            </div>
            <form class="flex flex-col gap-4 w-full h-full" onSubmit={onSubmit}>
                <div class="flex flex-col gap">
                    <label for="name" class="label text-sm">Name {item()?.productName}</label>
                </div> <div class="flex flex-col gap">
                    <label for="price" class="label text-sm">Price {item()?.productPrice}</label>
                </div> <div class="flex flex-col gap">
                    <label for="cost" class="label text-sm">Cost {item()?.productCost}</label>
                </div>
                <div class="flex flex-col gap">
                    <label for="category" class="label text-sm">Category {item()?.productCategory}</label>
                </div>
                <div class="flex flex-col gap">
                    <label for="quantity" class="label text-sm">Quantity</label>
                    <input type="number" id="quantity" class="input input-bordered" value={quantity()} onInput={(e) => setQuantity(e.currentTarget.value)} />
                </div>
                <button class="btn btn-primary" type="submit" disabled={disabled()}>Save</button>
            </form>
        </Layout>
    )
}
export default Item;
