import { createResource, createSignal, For } from "solid-js"
import Layout from "../components/Layout"
import { getProducts, Product } from "../utils/api"
import { useNavigate } from "@solidjs/router";

export const Products = () => {
    const [products] = createResource<Product[]>(getProducts);
    const [search, setSearch] = createSignal("");
    const navigate = useNavigate();


    const fileredProducts = () => {
        if (search() === "") {
            return products();
        }
        return products()?.filter((product) =>
            product.productName.toLowerCase().includes(search().toLowerCase())
            || product.productCost.toString().includes(search().toLowerCase())
            || product.productPrice.toString().includes(search().toLowerCase())
            || product.productCategory.toLowerCase().includes(search().toLowerCase())
            || product.productId.toLowerCase().includes(search().toLowerCase())
        );
    }

    const handleKeyDown = (e: KeyboardEvent, productId: string) => {
        if (e.key === "Enter" || e.key === " ") {
            e.preventDefault();
            navigate(`/products/${productId}`);
        }
    };

    return (
        <Layout>
            <div class="flex flex-col h-full w-full gap-4">
                <div class="px-2 flex flex-col gap-4">
                    <div class="flex flex-row justify-between items-center">
                        <h1 class="text-3xl font-bold"> Products </h1>
                        <a href="/products/new" class="btn btn-primary">New</a>
                    </div>
                    <label class="input input-bordered flex items-center gap-2">
                        <input type="text" class="grow" placeholder="Search by ID, Name, Price, Cost, Category..." value={search()} onInput={(e) => setSearch(e.currentTarget.value)} />
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
                            </tr>
                        </thead>
                        <tbody>
                            <For each={fileredProducts()}>
                                {(product) => (
                                    <tr class="hover rounded-md focus:outline-none focus:ring focus:ring-base-content/20 focus:ring-offset-base-200 focus:ring-offset-2"
                                        role="button"
                                        tabIndex="0"
                                        onKeyDown={(e) => handleKeyDown(e, product.productId)}
                                        onClick={() => navigate(`/products/${product.productId}`)}
                                    >
                                        <td>{product.productName}</td>
                                        <td>{product.productPrice}</td>
                                        <td>{product.productCost}</td>
                                        <td>{product.productCategory}</td>
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
export default Products
