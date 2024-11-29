import { useNavigate, useParams } from "@solidjs/router";
import Layout from "../components/Layout";
import Modal from "../components/Modal";
import Toast from "../components/Toast";
import { createEffect, createMemo, createResource, createSignal } from "solid-js";
import { deleteProduct, getProduct, Product, updateProduct } from "../utils/api";
import { MountableElement, Portal, render } from "solid-js/web";

const ProductPage = () => {
    const params = useParams();
    const navigate = useNavigate();

    const [product] = createResource<Product>(() => getProduct(params.productId));
    const [name, setName] = createSignal("");
    const [price, setPrice] = createSignal("");
    const [cost, setCost] = createSignal("");
    const [category, setCategory] = createSignal("");

    const goBack = async () => {
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
        const decimalPrice = parseFloat(price());
        const decimalCost = parseFloat(cost())
        const formattedPrice = parseFloat(decimalPrice.toFixed(2));
        const formattedCost = parseFloat(decimalCost.toFixed(2));

        await updateProduct(params.productId, name(), formattedPrice, formattedCost, category());
        const portalId = document.getElementById("portal");
        render(() => (
            <Portal>
                <Toast text={`${product()?.productName} successfully updated`} duration={2500} />
            </Portal>
        ), portalId as MountableElement);
        navigate("/products");
    }
    const confirmDelete = async () => {
        await deleteProduct(product()?.productId!);

        navigate("/products");
    }

    const disabled = createMemo(() => (name() === product()?.productName && price() === product()?.productPrice.toString() && cost() === product()?.productCost.toString() && category() === product()?.productCategory)
        || (name() === "" || price() === "" || cost() === "" || category() === ""));
    return (
        <Layout>
            <div class="flex flex-col gap-2 p-4">
                <h1 class="text-3xl font-bold"> FleurCo </h1>
                <div class="flex flex-row justify-between items-center">
                    <h2 class="text-3xl font-semi-bold"> Product Line </h2>
                    <div class="flex flex-row justify-center items-center">
                        <svg xmlns="http://www.w3.org/2000/svg" width="36" height="36" viewBox="0 0 24 24"><path d="M19 7.001c0 3.865-3.134 7-7 7s-7-3.135-7-7c0-3.867 3.134-7.001 7-7.001s7 3.134 7 7.001zm-1.598 7.18c-1.506 1.137-3.374 1.82-5.402 1.82-2.03 0-3.899-.685-5.407-1.822-4.072 1.793-6.593 7.376-6.593 9.821h24c0-2.423-2.6-8.006-6.598-9.819z" /></svg>                            <div class="flex flex-col justify-start items-start">
                            <label class="text-3x1 font-bold text-base"> Bryan Doe</label>
                            <label class="text-3x1 font-semi-bold text-sm">Warehouse Manager</label>
                        </div>
                    </div>
                </div>



                <div class="flex w-full justify-between items-center">
                    <div class="flex flex-col gap-2 items-start">
                        <Modal
                            btnClass="btn btn-sm btn-neutral"
                            btnText="Back"
                            confirmText={`Are you sure you want to go back? Any changes will not be saved`}
                            onSuccess={goBack}
                        />

                        <p class="text-2xl font-semibold">Product Info: {product()?.productName}</p>
                    </div>
                    <Modal
                        btnClass="btn btn-error btn-sm"
                        btnText="Delete"
                        successText="Product successfully deleted"
                        confirmText={`Do you want to delete product: ${product()?.productName}`}
                        onSuccess={confirmDelete}
                    />

                </div>
                <form class="flex flex-col gap-4 w-full min-h-fit" onSubmit={onSubmit}>
                    <div class="flex flex-col gap">
                        <label for="name" class="label text-sm">Name</label>
                        <input type="text" id="name" class="input input-bordered" value={name()} onInput={(e) => setName(e.currentTarget.value)} />
                    </div>
                    <div class="flex flex-col gap">
                        <label for="price" class="label text-sm">Price</label>
                        <input type="number" step="0.01" id="price" class="input input-bordered" value={price()} onInput={(e) => setPrice(e.currentTarget.value)} />
                    </div>
                    <div class="flex flex-col gap">
                        <label for="cost" class="label text-sm">Cost</label>
                        <input type="number" step="0.01" id="cost" class="input input-bordered" value={cost()} onInput={(e) => setCost(e.currentTarget.value)} />
                    </div>
                    <div class="flex flex-col gap">
                        <label for="category" class="label text-sm">Category</label>
                        <input type="text" id="category" class="input input-bordered" value={category()} onInput={(e) => setCategory(e.currentTarget.value)} />
                    </div>
                    <button class="btn btn-primary" type="submit" disabled={disabled()}>Save</button>
                </form>
            </div>
        </Layout >
    )
}
export default ProductPage;
