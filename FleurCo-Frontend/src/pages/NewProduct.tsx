import { useNavigate } from "@solidjs/router";
import Layout from "../components/Layout";
import { createMemo, createSignal } from "solid-js";
import { createProduct } from "../utils/api";

const NewProductPage = () => {
    const navigate = useNavigate();

    const [name, setName] = createSignal("");
    const [price, setPrice] = createSignal("");
    const [cost, setCost] = createSignal("");
    const [category, setCategory] = createSignal("");

    const goToProducts = () => {
        navigate("/products");
    }

    const onSubmit = async (e: Event) => {
        e.preventDefault();

        await createProduct(name(), parseInt(price()), parseInt(cost()), category());
        goToProducts();
    }

    const disabled = createMemo(() => name() === "" || price() === "" || cost() === "" || category() === "");
    return (
        <Layout>
            <div class="flex gap-4 w-full items-center">
                <button class="btn btn-sm btn-neutral" onClick={goToProducts}>Back</button>
                <p class="text-3xl font-semibold">New Product</p>
            </div>
            <form class="flex flex-col gap-4 w-full h-full" onSubmit={onSubmit}>
                <div class="flex flex-col gap">
                    <label for="name" class="label text-sm">Name</label>
                    <input required type="text" id="name" class="input input-bordered" value={name()} onInput={(e) => setName(e.currentTarget.value)} />
                </div>
                <div class="flex flex-col gap">
                    <label for="price" class="label text-sm">Price</label>
                    <input required type="number" id="price" class="input input-bordered" value={price()} onInput={(e) => setPrice(e.currentTarget.value)} />
                </div>
                <div class="flex flex-col gap">
                    <label for="cost" class="label text-sm">Cost</label>
                    <input required type="number" id="cost" class="input input-bordered" value={cost()} onInput={(e) => setCost(e.currentTarget.value)} />
                </div>
                <div class="flex flex-col gap">
                    <label for="category" class="label text-sm">Category</label>
                    <input required type="text" id="category" class="input input-bordered" value={category()} onInput={(e) => setCategory(e.currentTarget.value)} />
                </div>
                <button class="btn btn-primary" type="submit" disabled={disabled()}>Submit</button>
            </form>
        </Layout>
    )
}
export default NewProductPage;
