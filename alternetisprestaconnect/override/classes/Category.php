<?php
/**
 * Copyright since 2007 PrestaShop SA and Contributors
 * PrestaShop is an International Registered Trademark & Property of PrestaShop SA
 *
 * NOTICE OF LICENSE
 *
 * This source file is subject to the Open Software License (OSL 3.0)
 * that is bundled with this package in the file LICENSE.md.
 * It is also available through the world-wide-web at this URL:
 * https://opensource.org/licenses/OSL-3.0
 * If you did not receive a copy of the license and are unable to
 * obtain it through the world-wide-web, please send an email
 * to license@prestashop.com so we can send you a copy immediately.
 *
 * DISCLAIMER
 *
 * Do not edit or add to this file if you wish to upgrade PrestaShop to newer
 * versions in the future. If you wish to customize PrestaShop for your
 * needs please refer to https://devdocs.prestashop.com/ for more information.
 *
 * @author    PrestaShop SA and Contributors <contact@prestashop.com>
 * @copyright Since 2007 PrestaShop SA and Contributors
 * @license   https://opensource.org/licenses/OSL-3.0 Open Software License (OSL 3.0)
 */


 declare(strict_types=1);



/**
 * Class Overide CategoryCore.
 */

 class Category extends CategoryCore
 {

    /**
     * Alternetis CategoryCore constructor.
     *
     * @param int|null $idCategory
     * @param int|null $idLang
     * @param int|null $idShop
     */
    public function __construct($idCategory = null, $idLang = null, $idShop = null)
    {
        parent::__construct($idCategory, $idLang, $idShop);
        $this->image_dir = _PS_CAT_IMG_DIR_;
        $this->id_image = ($this->id && file_exists($this->image_dir . (int) $this->id . '.jpg')) ? (int) $this->id : false;
        if (defined('PS_INSTALLATION_IN_PROGRESS')) {
            $this->doNotRegenerateNTree = true;
        }
    }


     /**
     * Adds current Category as a new Object to the database. 
     * Implement add for shop
     *
     * @param bool $autoDate Automatically set `date_upd` and `date_add` columns
     * @param bool $nullValues Whether we want to use NULL values instead of empty quotes values
     *
     * @return bool Indicates whether the Category has been successfully added
     *
     * @throws PrestaShopDatabaseException
     * @throws PrestaShopException
     */
    public function add($autoDate = true, $nullValues = false)
    {
        if (!isset($this->level_depth)) {
            $this->level_depth = $this->calcLevelDepth();
        }

        if ($this->is_root_category && ($idRootCategory = (int) Configuration::get('PS_ROOT_CATEGORY'))) {
            $this->id_parent = $idRootCategory;
        }

        $ret = ObjectModel::add($autoDate, $nullValues); // Appelle la méthode add de CategoryCore

        if (Tools::isSubmit('checkBoxShopAsso_category')) 
        {
            foreach (Tools::getValue('checkBoxShopAsso_category') as $idShop => $value) 
            {
                $position = (int) Category::getLastPosition((int) $this->id_parent, $idShop);
                $this->addPosition($position, $idShop);
            }
        } else 
        {
            $currentShopId = $this->id_shop ?? null;
            if($currentShopId == null)
            {
                foreach (Shop::getShops(true) as $shop) {
                    $position = (int) Category::getLastPosition((int) $this->id_parent, $shop['id_shop']);
                    $this->addPosition($position, $shop['id_shop']);
                }
            }else
            {
                $position = (int) Category::getLastPosition((int) $this->id_parent, $currentShopId);
                $this->addPosition($position,  $currentShopId);
            }  
        }

        if (!$this->doNotRegenerateNTree) {
            Category::regenerateEntireNtree();
        }
        // if access group is not set, initialize it with 3 default groups
        $this->updateGroup(($this->groupBox !== null) ? $this->groupBox : []);
        Hook::exec('actionCategoryAdd', ['category' => $this]);

        return $ret ;
    }

     /**
     * Updates the current object in the database.
     * Implement Update For Shop
     *
     * @param bool $nullValues Whether we want to use NULL values instead of empty quotes values
     *
     * @return bool Indicates whether the CartRule has been successfully updated
     *
     * @throws PrestaShopDatabaseException
     * @throws PrestaShopException
     */
    public function update($nullValues = false)
    {
        if ($this->id_parent == $this->id) {
            throw new PrestaShopException('a category cannot be its own parent');
        }

        if ($this->is_root_category && $this->id_parent != (int) Configuration::get('PS_ROOT_CATEGORY')) {
            $this->is_root_category = 0;
        }

        // Update group selection
        $this->updateGroup($this->groupBox);

        if ($this->level_depth != $this->calcLevelDepth()) {
            $this->level_depth = $this->calcLevelDepth();
            $changed = true;
        }

        // If the parent category was changed, we don't want to have 2 categories with the same position
        if (!isset($changed)) {
            $changed = $this->getDuplicatePosition();
        }

        if ($changed) {
            if (Tools::isSubmit('checkBoxShopAsso_category')) {
                foreach (Tools::getValue('checkBoxShopAsso_category') as $idAssoObject => $idShop) {
                    $this->addPosition($this->position, (int) $idShop);
                }
            } else 
            {
                $currentShopId = $this->id_shop ?? null;
                if($currentShopId == null)
                {
                    foreach (Shop::getShops(true) as $shop) {
                        $this->addPosition($this->position, $shop['id_shop']);
                    }
                }else
                {
                    $this->addPosition($this->position,  $currentShopId);
                }
            }
        }

        $ret = ObjectModel::update($nullValues);
        if ($changed && !$this->doNotRegenerateNTree) {
            $this->cleanPositions((int) $this->id_parent);
            Category::regenerateEntireNtree();
            $this->recalculateLevelDepth($this->id);
        }
        Hook::exec('actionCategoryUpdate', ['category' => $this]);

        return $ret;
    }


 }