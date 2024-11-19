const API_URL = 'http://localhost:5219';

export interface Product {
    productId: string;
    productName: string;
    productPrice: number;
    productCost: number;
    productCategory: string;
}
export interface Order {
    orderId: string;
    orderDate: string;
    orderType: string;
    orderStatus: string;
    orderTotal: number;
}
export interface OrderProduct extends Product {
    productQuantity: number
}
export interface InventoryProduct extends Product {
    inventoryId: string;
    quantity: number;
}

export const getInventory = async (): Promise<InventoryProduct[]> => {
    const response = await fetch(`${API_URL}/inventory`);

    if (!response.ok) {
        throw new Error('Failed to fetch inventory')
    }
    return response.json();
};
export const getItem = async (inventoryId: string): Promise<InventoryProduct> => {
    const response = await fetch(`${API_URL}/inventory/${inventoryId}`);

    if (!response.ok) {
        throw new Error('Failed to fetch inventory')
    }
    return response.json();
};
export const updateQuantity = async (inventoryId: string, quantity: number): Promise<InventoryProduct> => {
    const response = await fetch(`${API_URL}/inventory/${inventoryId}`, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            quantity: quantity
        }),
    });

    if (!response.ok) {
        throw new Error('Failed to update product');
    }

    return response.json();
}

export const getProducts = async (): Promise<Product[]> => {
    const response = await fetch(`${API_URL}/products`);

    if (!response.ok) {
        throw new Error('Failed to fetch item');
    }

    return response.json();
};

export const getProduct = async (productId: string): Promise<Product> => {
    const response = await fetch(`${API_URL}/products/${productId}`);

    if (!response.ok) {
        throw new Error('Failed to fetch product');
    }

    return response.json();
};

export const deleteProduct = async (productId: string): Promise<void> => {
    const response = await fetch(`${API_URL}/products/${productId}`, {
        method: 'DELETE',
    });

    if (!response.ok) {
        throw new Error('Failed to delete product');
    }
};

export const createProduct = async (name: string, price: number, cost: number, category: string): Promise<Product> => {
    const response = await fetch(`${API_URL}/products`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            productName: name,
            productPrice: price,
            productCost: cost,
            productCategory: category,
        }),
    });

    if (!response.ok) {
        throw new Error('Failed to create product');
    }

    return response.json();
}
export const updateProduct = async (id: string, name: string, price: number, cost: number, category: string): Promise<Product> => {
    const response = await fetch(`${API_URL}/products/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            productName: name,
            productPrice: price,
            productCost: cost,
            productCategory: category,
        }),
    });

    if (!response.ok) {
        throw new Error('Failed to update product');
    }

    return response.json();
}

export const getOrders = async (): Promise<Order[]> => {
    const response = await fetch(`${API_URL}/orders`);

    if (!response.ok) {
        throw new Error('Failed to fetch orders');
    }

    return response.json();
};

export const getOrder = async (orderId: string): Promise<Order> => {
    const response = await fetch(`${API_URL}/orders/${orderId}`);

    if (!response.ok) {
        throw new Error('Failed to fetch order');
    }

    return response.json();
};
// export const createBackorder = async (name: string, price: number, cost: number, category: string): Promise<OrderProduct[]> => {
//     const response = await fetch(`${API_URL}/backorders`, {
//         method: 'POST',
//         headers: {
//             'Content-Type': 'application/json',
//         },
//         body: JSON.stringify({
//             inventoryId:
//         }),
//     });

//     if (!response.ok) {
//         throw new Error('Failed to create product');
//     }

//     return response.json();
// }
