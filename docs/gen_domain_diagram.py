# -*- coding: utf-8 -*-
"""Gera o diagrama da modelagem do dominio (DDD) do ShopFlexByte em SVG e PNG."""
import os
from xml.sax.saxutils import escape

W, H = 1660, 1240
parts = []

# ---- paleta ----
BG = "#f1f5f9"
INK = "#0f172a"
SUB = "#475569"
ROOT_HDR = "#1e3a8a"
ENTITY = ("#dbeafe", "#3b82f6")
VO = ("#dcfce7", "#16a34a")
REPO = ("#ffedd5", "#ea580c")
FACTORY = ("#ede9fe", "#7c3aed")
ENUM = ("#f1f5f9", "#64748b")
ACL = ("#fee2e2", "#dc2626")
CTX = "#94a3b8"

FONT = "Segoe UI, Helvetica, Arial, sans-serif"


def esc(s):
    return escape(str(s))


def rect(x, y, w, h, fill, stroke, rx=10, sw=1.5, dash=None, op=1.0):
    d = f' stroke-dasharray="{dash}"' if dash else ""
    parts.append(
        f'<rect x="{x}" y="{y}" width="{w}" height="{h}" rx="{rx}" ry="{rx}" '
        f'fill="{fill}" stroke="{stroke}" stroke-width="{sw}"{d} opacity="{op}"/>'
    )


def text(x, y, s, size=14, fill=INK, weight="normal", anchor="start", italic=False, family=FONT):
    st = ' font-style="italic"' if italic else ""
    parts.append(
        f'<text x="{x}" y="{y}" font-family="{family}" font-size="{size}" '
        f'fill="{fill}" font-weight="{weight}" text-anchor="{anchor}"{st}>{esc(s)}</text>'
    )


def line(x1, y1, x2, y2, stroke=SUB, sw=1.5, dash=None, marker=True):
    d = f' stroke-dasharray="{dash}"' if dash else ""
    m = ' marker-end="url(#arrow)"' if marker else ""
    parts.append(
        f'<line x1="{x1}" y1="{y1}" x2="{x2}" y2="{y2}" stroke="{stroke}" '
        f'stroke-width="{sw}"{d}{m}/>'
    )


def stereo(x, y, label, size=11, fill=SUB, anchor="start"):
    text(x, y, f"« {label} »", size=size, fill=fill, italic=True, anchor=anchor)


# ---------- estruturas ----------
def aggregate(x, y, w, name, lines, methods):
    """UML-like box para Aggregate Root."""
    head = 46
    body = 22 + len(lines) * 19 + (8 + len(methods) * 19 if methods else 0)
    h = head + body
    rect(x, y, w, h, "#ffffff", ENTITY[1], rx=10, sw=2)
    rect(x, y, w, head, ROOT_HDR, ROOT_HDR, rx=10, sw=0)
    rect(x, y + head - 12, w, 12, ROOT_HDR, ROOT_HDR, rx=0, sw=0)
    stereo(x + w / 2, y + 19, "Aggregate Root  |  Entity", size=11, fill="#bfdbfe", anchor="middle")
    text(x + w / 2, y + 38, name, size=17, fill="#ffffff", weight="bold", anchor="middle")
    cy = y + head + 20
    for ln in lines:
        text(x + 14, cy, ln, size=12.5, fill=INK)
        cy += 19
    if methods:
        line(x + 8, cy - 12, x + w - 8, cy - 12, stroke="#cbd5e1", sw=1, marker=False)
        cy += 0
        for m in methods:
            text(x + 14, cy, m, size=12.5, fill="#1d4ed8")
            cy += 19
    return h


def pill(x, y, w, text_lines, kind, stereo_label):
    fill, stroke = kind
    h = 24 + len(text_lines) * 18
    rect(x, y, w, h, fill, stroke, rx=8, sw=1.5)
    stereo(x + 10, y + 16, stereo_label, size=10, fill=stroke)
    cy = y + 34
    for ln in text_lines:
        text(x + 10, cy, ln, size=12, fill=INK)
        cy += 18
    return h


# ================= CANVAS =================
parts.append(
    f'<svg xmlns="http://www.w3.org/2000/svg" width="{W}" height="{H}" '
    f'viewBox="0 0 {W} {H}">'
)
parts.append(
    '<defs><marker id="arrow" markerWidth="10" markerHeight="10" refX="8" refY="3" '
    'orient="auto" markerUnits="strokeWidth">'
    '<path d="M0,0 L8,3 L0,6 Z" fill="#475569"/></marker></defs>'
)
rect(0, 0, W, H, BG, BG, rx=0, sw=0)

# ----- titulo -----
text(40, 46, "ShopFlexByte — Modelagem do Domínio (DDD)", size=28, fill=INK, weight="bold")
text(40, 72, "Bounded Contexts, Aggregates, Entities, Value Objects, Repositories, Factories e Anti-Corruption Layer",
     size=14, fill=SUB)

# ================= COLUNAS / CONTEXTOS =================
col_x = [40, 588, 1136]
cw = 484
row_y = [100, 470]
ch = 348


def context_card(cx, cy, title):
    rect(cx, cy, cw, ch, "#ffffff", CTX, rx=14, sw=1.6, dash="7 5", op=1)
    rect(cx, cy, cw, 34, "#e2e8f0", CTX, rx=14, sw=0)
    rect(cx, cy + 22, cw, 12, "#e2e8f0", "#e2e8f0", rx=0, sw=0)
    text(cx + 16, cy + 23, title, size=15, fill=INK, weight="bold")
    stereo(cx + cw - 14, cy + 23, "Bounded Context", size=11, fill=SUB, anchor="end")


# ---- 1. Clientes ----
cx, cy = col_x[0], row_y[0]
context_card(cx, cy, "Gestão de Clientes")
aggregate(cx + 16, cy + 46, cw - 32, "Customer",
          ["FullName, Cpf, Email : VO", "PrimaryAddress, OtherAddresses[]"],
          ["Update()  Delete()", "SetPrimaryAddress()  AddOtherAddress()"])
py = cy + 232
pill(cx + 16, py, 224, ["FullName  Cpf", "Email  Address"], VO, "Value Objects")
pill(cx + 250, py, 218, ["Customer.Create()"], FACTORY, "Factory")
pill(cx + 16, py + 64, cw - 32, ["ICustomerRepository"], REPO, "Repository")

# ---- 2. Identidade & Acesso ----
cx, cy = col_x[1], row_y[0]
context_card(cx, cy, "Identidade & Acesso")
aggregate(cx + 16, cy + 46, cw - 32, "User",
          ["Username, Email, FullName", "Roles : UserRole[]"],
          ["AddRole()  RemoveRole()"])
py = cy + 213
pill(cx + 16, py, 224, ["UserRole", "CustomerService | Admin"], ENUM, "Enum")
pill(cx + 250, py, 218, ["IUserRepository"], REPO, "Repository")

# ---- 3. Catalogo ----
cx, cy = col_x[2], row_y[0]
context_card(cx, cy, "Catálogo de Produtos")
aggregate(cx + 16, cy + 46, cw - 32, "Product",
          ["Name, Price, StockLevel", "CategoryId"],
          ["UpdateStockLevel()"])
py = cy + 213
pill(cx + 16, py, 224, ["Category", "Id, Name"], ENTITY, "Entity")
pill(cx + 250, py, 218, ["IProductRepository"], REPO, "Repository")

# ---- 4. Carrinho ----
cx, cy = col_x[0], row_y[1]
context_card(cx, cy, "Carrinho de Compras")
aggregate(cx + 16, cy + 46, cw - 32, "ShoppingCart",
          ["UserId", "Items[]"],
          ["AddItem()  RemoveItem()"])
py = cy + 213
pill(cx + 16, py, 224, ["ShoppingCartItem", "ProductId, Price, Qty"], ENTITY, "Entity (filha)")
pill(cx + 250, py, 218, ["IShoppingCartRepository"], REPO, "Repository")

# ---- 5. Pedidos ----
cx, cy = col_x[1], row_y[1]
context_card(cx, cy, "Pedidos")
aggregate(cx + 16, cy + 46, cw - 32, "Order",
          ["UserId, TotalAmount", "Status : OrderStatus"],
          ["(ciclo de vida do pedido)"])
py = cy + 213
pill(cx + 16, py, 224, ["OrderItem", "ProductId, Price, Qty"], VO, "Value Object")
pill(cx + 250, py, 218, ["IOrderRepository"], REPO, "Repository")

# ---- 6. Pagamento (externo) ----
cx, cy = col_x[2], row_y[1]
context_card(cx, cy, "Pagamento (Externo)")
rect(cx + 16, cy + 50, cw - 32, 70, "#ffffff", ENTITY[1], rx=10, sw=2)
stereo(cx + cw / 2, cy + 73, "Port  |  Interface", size=11, fill=SUB, anchor="middle")
text(cx + cw / 2, cy + 95, "IPaymentGateway", size=16, fill=INK, weight="bold", anchor="middle")
text(cx + cw / 2, cy + 112, "ProcessPayment()  GetStatus()", size=11.5, fill=SUB, anchor="middle")
line(cx + cw / 2, cy + 120, cx + cw / 2, cy + 150)
pill(cx + 16, cy + 152, cw - 32, ["PaymentGateway  (adapter via Refit)",
                                  "traduz a API externa de pagamento"], ACL, "Anti-Corruption Layer")
text(cx + cw / 2, cy + 250, "API de Pagamento de Terceiros", size=12, fill=SUB, anchor="middle", italic=True)
rect(cx + 96, cy + 258, cw - 192, 34, "#e2e8f0", CTX, rx=8, sw=1.3, dash="5 4")
text(cx + cw / 2, cy + 280, "Sistema Externo", size=12, fill=SUB, anchor="middle")

# ================= BANDA ACL / SHARED KERNEL =================
by = 838
rect(40, by, W - 80, 150, "#ffffff", ACL[1], rx=14, sw=1.8, dash="8 5")
text(60, by + 30, "Context Map  —  Integração entre Contextos", size=17, fill=INK, weight="bold")
stereo(W - 60, by + 30, "Anti-Corruption Layer  +  Shared Kernel", size=12, fill=ACL[1], anchor="end")

# ACL mapping
rect(60, by + 48, 760, 86, ACL[0], ACL[1], rx=10, sw=1.5)
stereo(76, by + 70, "Anti-Corruption Layer (AutoMapper)", size=11, fill=ACL[1])
text(76, by + 92, "InfrastructureMappingProfile", size=14, fill=INK, weight="bold")
text(76, by + 113, "Persistence.Entities (EF/SQL)  ⟷  Domain.Entities (modelo rico)", size=12.5, fill=SUB)
text(76, by + 130, "Impede que detalhes do banco vazem para o domínio.", size=11.5, fill=SUB, italic=True)

# Shared kernel
rect(840, by + 48, W - 880, 86, "#eef2ff", "#6366f1", rx=10, sw=1.5)
stereo(856, by + 70, "Shared Kernel  /  Building Blocks", size=11, fill="#6366f1")
text(856, by + 92, "Common: Entity  ·  ValueObject  ·  Result", size=14, fill=INK, weight="bold")
text(856, by + 113, "Base de identidade/igualdade reutilizada por todos os Aggregates", size=12.5, fill=SUB)
text(856, by + 130, "e Value Objects dos diferentes Bounded Contexts.", size=12.5, fill=SUB)

# ================= LEGENDA =================
ly = 1010
text(40, ly + 6, "Legenda:", size=14, fill=INK, weight="bold")
legend = [
    ("Aggregate Root / Entity", ENTITY),
    ("Value Object", VO),
    ("Repository", REPO),
    ("Factory", FACTORY),
    ("Enum", ENUM),
    ("Anti-Corruption Layer", ACL),
]
lx = 130
for label, (fill, stroke) in legend:
    rect(lx, ly - 12, 22, 22, fill, stroke, rx=5, sw=1.5)
    text(lx + 30, ly + 5, label, size=13, fill=INK)
    lx += 60 + len(label) * 8.2

# nota ubiquitous language / domain services
text(40, ly + 44,
     "Ubiquitous Language: os nomes (Customer, ShoppingCart, Order, Cpf...) refletem a linguagem do negócio.",
     size=12.5, fill=SUB, italic=True)
text(40, ly + 66,
     "Factories (Customer.Create, Address.Create) encapsulam a construção válida dos objetos; "
     "a orquestração entre Aggregates fica nos Use Cases (camada de Aplicação).",
     size=12.5, fill=SUB, italic=True)

parts.append("</svg>")

svg = "\n".join(parts)
out_dir = os.path.dirname(os.path.abspath(__file__))
svg_path = os.path.join(out_dir, "domain-model.svg")
png_path = os.path.join(out_dir, "domain-model.png")
with open(svg_path, "w", encoding="utf-8") as f:
    f.write(svg)
print("SVG:", svg_path)

try:
    import cairosvg
    cairosvg.svg2png(url=svg_path, write_to=png_path, output_width=W * 2, output_height=H * 2)
    print("PNG:", png_path)
except Exception as e:
    print("PNG falhou:", e)
