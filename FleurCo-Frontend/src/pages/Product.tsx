import { useNavigate, useParams } from "@solidjs/router";
import Layout from "../components/Layout";
import { createEffect, createMemo, createResource, createSignal } from "solid-js";
import { deleteProduct, getProduct, Product, updateProduct } from "../utils/api";

const ProductPage = () => {
    const params = useParams();
    const navigate = useNavigate();

    const [product] = createResource<Product>(() => getProduct(params.productId));
    const [name, setName] = createSignal("");
    const [price, setPrice] = createSignal("");
    const [cost, setCost] = createSignal("");
    const [category, setCategory] = createSignal("");

    const goBack = () => {
        navigate("/products");
    }

    createEffect(() => {
        if (product()) {
            setName(product()?.productName!);
            setPrice(product()?.productPrice.toString()!);
            setCost(product()?.productCost.toString()!);
            setCategory(product()?.productCategory!);
        }
    });

    const onSubmit = async (e: Event) => {
        e.preventDefault();

        await updateProduct(params.productId, name(), parseInt(price()), parseInt(cost()), category());
        navigate("/products");
    }
    const confirmDelete = async (e: Event) => {
        e.preventDefault();
        const confirmed = confirm(`Are you sure you want to delete product ${product()?.productName}?`)
        if (!confirmed) return;
        await deleteProduct(product()?.productId!);
        navigate("/products");
    }

    const disabled = createMemo(() => (name() === product()?.productName && price() === product()?.productPrice.toString() && cost() === product()?.productCost.toString() && category() === product()?.productCategory)
        || (name() === "" || price() === "" || cost() === "" || category() === ""));
    return (
        <Layout>
            <div class="flex w-full justify-between items-center">
                <div class="flex gap-4">
                    <button class="btn btn-sm btn-neutral" onClick={goBack}>Back</button>
                    <p class="text-3xl font-semibold">Updating: {product()?.productName}</p>
                </div>
                <button class="btn btn-error btn-sm" onClick={confirmDelete}>Delete</button>
            </div>
            <form class="flex flex-col gap-4 w-full h-full" onSubmit={onSubmit}>
                <div class="flex flex-col gap">
                    <label for="name" class="label text-sm">Name</label>
                    <input type="text" id="name" class="input input-bordered" value={name()} onInput={(e) => setName(e.currentTarget.value)} />
                </div>
                <div class="flex flex-col gap">
                    <label for="price" class="label text-sm">Price</label>
                    <input type="number" id="price" class="input input-bordered" value={price()} onInput={(e) => setPrice(e.currentTarget.value)} />
                </div>
                <div class="flex flex-col gap">
                    <label for="cost" class="label text-sm">Cost</label>
                    <input type="number" id="cost" class="input input-bordered" value={cost()} onInput={(e) => setCost(e.currentTarget.value)} />
                </div>
                <div class="flex flex-col gap">
                    <label for="category" class="label text-sm">Category</label>
                    <input type="text" id="category" class="input input-bordered" value={category()} onInput={(e) => setCategory(e.currentTarget.value)} />
                </div>
                <button class="btn btn-primary" type="submit" disabled={disabled()}>Save</button>
            </form>
        </Layout>
    )
}
export default ProductPage;
